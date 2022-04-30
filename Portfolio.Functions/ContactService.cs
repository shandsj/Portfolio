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
    /// <summary>
    /// Service that hosts the Azure Functions for sending a contact email.
    /// </summary>
    public static class ContactService
    {
        [FunctionName("ContactMe")]
        public static async Task<IActionResult> ContactMeAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "contact")] HttpRequest req,
            [SendGrid(ApiKey = "SENDGRID_API_KEY")] IAsyncCollector<SendGridMessage> messages,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Contact contact = JsonConvert.DeserializeObject<Contact>(requestBody);

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.AddTo(Environment.GetEnvironmentVariable("MY_EMAIL_ADDRESS", EnvironmentVariableTarget.Process));
            sendGridMessage.AddContent("text/html", contact.Message);
            sendGridMessage.SetSubject($"Your portfolio received a message from {contact.Name} ({contact.Email})");
            sendGridMessage.SetFrom(Environment.GetEnvironmentVariable("MY_EMAIL_ADDRESS", EnvironmentVariableTarget.Process));
            await messages.AddAsync(sendGridMessage);

            return new OkResult();
        }
    }
}
