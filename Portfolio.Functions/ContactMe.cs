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
using SendGrid.Helpers.Mail;

namespace Portfolio.Functions
{
    public static class ContactMe
    {
        [FunctionName("ContactMe")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [SendGrid(ApiKey = "SENDGRID_API_KEY")] IAsyncCollector<SendGridMessage> messages,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Message message = JsonConvert.DeserializeObject<Message>(requestBody);

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.AddTo("jason.shands@gmail.com");
            sendGridMessage.AddContent("text/html", message.Text);
            sendGridMessage.SetSubject($"Your portfolio received a message from {message.Name} ({message.Email})");
            sendGridMessage.SetFrom("jason.shands@gmail.com");
            await messages.AddAsync(sendGridMessage);

            return new OkResult();
        }
    }
}
