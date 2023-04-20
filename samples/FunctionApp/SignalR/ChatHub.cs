using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp.SignalR
{
    public static class Hub
    {
        [Function("SignalRTrigger2")]
        public static void SendMessage(FunctionContext context, [SignalRTrigger("hub", "connection", "SendMessage", ConnectionStringSetting = "MyConnection")] SignalRInvocationContext invocationContext, string messge)
        {
            context.GetLogger("SignalRTrigger").LogInformation(JsonSerializer.Serialize(invocationContext, new JsonSerializerOptions
            {
                WriteIndented = true
            }));
            context.GetLogger("SignalRTrigger").LogInformation(JsonSerializer.Serialize(messge, new JsonSerializerOptions
            {
                WriteIndented = true
            }));
        }
    }
}
