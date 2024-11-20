using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Services.PayOSObjects;

namespace Application.Interfaces
{
    public interface IPayOSService
    {
        Task<string> CreateLink(int depositMoney, int walletId);
        Task<WebhookResponse> ReturnWebhook2(WebhookType webhookBody);
    }
}
