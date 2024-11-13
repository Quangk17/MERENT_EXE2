using Application.Interfaces;
using Application.ServiceRespones;
using Application.Services;
using Application.ViewModels.WalletDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MERENT_API.Controllers
{
    public class WalletController : BaseController
    {
        private readonly IWalletService _walletService;
        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;
        private readonly IPayOSService _payOSService;
        private readonly ILogger<WalletController> _logger;

        public WalletController(IWalletService walletService, IMapper mapper, IClaimsService claimsService, IPayOSService payOSService, ILogger<WalletController> logger)
        {
            _walletService = walletService;
            _mapper = mapper;          
            _claimsService = claimsService;    
            _payOSService = payOSService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllWallets()
        {
            var result = await _walletService.GetWalletsAsync();
            return Ok(result);
        }

        [HttpGet("user-wallet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListWalletByUserId()
        {
            try
            {
                var userIdString = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;


                if (userIdString == null || !int.TryParse(userIdString, out int userId))
                {
                    throw new Exception("User Id is invalid");
                }
                var result = await _walletService.GetWalletByUserId(userId);
                return Ok(ServiceResponse<List<WalletDTO>>.Succeed(result, "Get 2 Wallet Of User with Id " + userId + " Successfully!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResponse<object>.Fail(ex));
            }
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWalletById(int id)
        {
            var result = await _walletService.GetWalletByIdAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateWallet([FromBody] WalletCreateDTO createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var result = await _walletService.CreateWalletAsync(createDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateWallet(int id, [FromBody] WalletUpdateDTO updateDto)
        {
            var result = await _walletService.UpdateWalletAsync(id, updateDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteWallet(int id)
        {
            var result = await _walletService.DeleteWalletAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [Route("create-payment-link-payos")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepositRequestDTO depositRequest)
        {
            try
            {
                var userIdString = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdString))
                {
                    throw new Exception("UserId is invalid or you are not logged in");
                }
                int userId = int.Parse(userIdString);


                if (userId == null)
                {
                    throw new Exception("UserId is invalid or you are not login");
                }
                if (depositRequest.Amount <= 0)
                {
                    throw new Exception("Amount is invalid");
                }
                var result = await _walletService.Deposit(userId, depositRequest.Amount, "PayOS");

                if (result == null)
                {
                    throw new Exception("Create Deposit Transaction Failed!");
                }
                else
                {
                    var url = await _payOSService.CreateLink(depositRequest.Amount);

                    return Ok(ServiceResponse<string>.Succeed(url, "Payment to deposit!"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResponse<object>.Fail(ex));
            }
        }



        [HttpPost("hook")]
        public async Task<IActionResult> ReceiveWebhook([FromBody] WebhookType webhookBody)
        {
            try
            {
                var result = await _payOSService.ReturnWebhook2(webhookBody);

                if (result.Success)
                {
                    return Ok(new { Message = "Webhook processed successfully" });
                }

                return BadRequest(new { Message = "Webhook processing failed." });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }

}
