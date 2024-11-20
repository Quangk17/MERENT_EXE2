using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Application.Services.PayOSObjects;
using Net.payOS;
using Net.payOS.Types;
using Domain.Enums;
using Domain.Entites;

namespace Application.Services
{
    public class PayOSService : IPayOSService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PayOSService> _logger;
        private readonly PayOS _payOS;


        public PayOSService(ILogger<PayOSService> logger , IUnitOfWork unitOfWork)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            var ClientId = configuration["PayOS:ClientId"];
            var ChecksumKey = configuration["PayOS:ChecksumKey"];
            var ApiKey = configuration["PayOS:ApiKey"];
            _payOS = new PayOS(ClientId, ApiKey, ChecksumKey);
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CreateLink(int depositMoney)
        {
            var domain = "https://merent-fe.vercel.app/#/profile";

            var paymentLinkRequest = new PaymentData(
                orderCode: int.Parse(DateTimeOffset.Now.ToString("ffffff")),
                amount: depositMoney,
                description: "Nạp tiền: " + depositMoney,
                items: [new("Nạp tiền " + depositMoney, 1, depositMoney)],
                returnUrl: domain + "?success=true&transactionId=" + "GG" + "&amount=" + depositMoney,
                cancelUrl: domain + "?canceled=true&transactionId=" + "GG" + "&amount=" + depositMoney
            );
            var response = await _payOS.createPaymentLink(paymentLinkRequest);

            return response.checkoutUrl;
        }

        public async Task<WebhookResponse> ReturnWebhook2(WebhookType webhookType)
        {
            try
            {
                // Log the receipt of the webhook
                //Seriablize the object to log
                _logger.LogInformation(JsonConvert.SerializeObject(webhookType));

                //WebhookData verifiedData = _payOS.verifyPaymentWebhookData(webhookType); //xác thực data from webhook
                //string responseCode = verifiedData.code;
                //string orderCode = verifiedData.orderCode.ToString();
                //string transactionId = "TRANS" + orderCode;
                var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(int.Parse(webhookType.data.orderCode.ToString()));


                // Handle the webhook based on the transaction status
                switch (webhookType.data.code)
                {
                    case "00":
                        // Update the transaction status
                        await UpdateStatusTransaction(transaction);

                        return new WebhookResponse
                        {
                            Success = true,
                            Note = "Payment processed successfully"
                        };

                    case "01":
                        // Update the transaction status
                        await UpdateErrorTransaction(transaction, "Payment failed");

                        return new WebhookResponse
                        {
                            Success = false,
                            Note = "Invalid parameters"
                        };

                    default:
                        return new WebhookResponse
                        {
                            Success = false,
                            Note = "Unhandled code"
                        };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
            
        }
        private async Task UpdateStatusTransaction(Domain.Entites.Transaction transaction)
        {
            transaction.Status = TransactionStatusEnums.SUCCESS.ToString();
            //Plus money to user wallet
            var wallet = await _unitOfWork.WalletRepository.GetListWalletByUserId(transaction.WalletId);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateErrorTransaction(Domain.Entites.Transaction transaction, string note)
        {
            transaction.Status = TransactionStatusEnums.FAILED.ToString();
            await _unitOfWork.SaveChangeAsync();
        }
    }

    public static class PayOSUtils
    {
        public static bool IsValidData(WebhookType payOSWebhook, string transactionSignature, string ChecksumKey)
        {
            try
            {
                JObject jsonObject = JObject.Parse(JsonConvert.SerializeObject(payOSWebhook.data)); // Đảm bảo chuyển thành JSON hợp lệ
                var sortedKeys = jsonObject.Properties().Select(p => p.Name).OrderBy(k => k).ToList();

                StringBuilder transactionStr = new StringBuilder();
                foreach (var key in sortedKeys)
                {
                    string value = jsonObject[key]?.ToString() ?? string.Empty;
                    transactionStr.Append($"{key}={value}");
                    if (key != sortedKeys.Last())
                    {
                        transactionStr.Append("&");
                    }
                }

                string signature = ComputeHmacSha256(transactionStr.ToString(), ChecksumKey);
                return signature.Equals(transactionSignature, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }


        public static string ComputeHmacSha256(string data, string key)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
    public class PayOSObjects
    {
        public class PayOSWebhookResponse
        {
            public bool Success { get; set; }
            public PayOSTransaction Data { get; set; }
            public string Note { get; set; }
        }
        public class PayOSWebhook
        {
            public string Code { get; set; }
            public string Desc { get; set; }
            public bool Success { get; set; }
            public PayOSTransaction Data { get; set; }
            public string Signature { get; set; }
        }

        public class PayOSTransaction
        {
 
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public string AccountNumber { get; set; }
            public string Reference { get; set; }
            public string TransactionDateTime { get; set; }
            public string Currency { get; set; }
            public string PaymentLinkId { get; set; }
            public string Code { get; set; }
            public string Desc { get; set; }
            public string CounterAccountBankId { get; set; }
            public string CounterAccountBankName { get; set; }
            public string CounterAccountName { get; set; }
            public string CounterAccountNumber { get; set; }
            public string VirtualAccountName { get; set; }
            public string VirtualAccountNumber { get; set; }
            //public string TransactionId { get; set; }
        }

        public class WebhookResponse
        {
            public bool Success { get; set; }
            public string Note { get; set; }
        }
    }
}
