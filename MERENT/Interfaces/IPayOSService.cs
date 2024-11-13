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
        Task<string> CreateLink(int depositMoney);
        Task<PayOSWebhookResponse> ReturnWebhook(PayOSWebhook payOSWebhook);
        Task<WebhookResponse> ReturnWebhook2(WebhookType webhookBody);
    }
}
