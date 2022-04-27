using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Portfolio.Domain;
using System.Threading;

namespace Portfolio.Functions
{
    public static class SendEmail
    {
        [FunctionName("ContactMe")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            string email = req.Query["email"];
            string subject = req.Query["subject"];
            string text = req.Query["text"];            

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.Name;
            email = email ?? data?.Email;
            text = text ?? data?.Message;

            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                var message = new Message(name, email, subject, text);
                var service = new MessagingService();
                await service.SendMessageAsync(message, cts.Token);
                log.LogInformation("Message sent.");
            }
            catch (Exception e)
            {
                return new NotFoundObjectResult(e);
            }

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
