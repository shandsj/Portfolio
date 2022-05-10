using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;

namespace Portfolio.Functions.Contacts;

/// <summary>
/// Service that hosts the Azure Functions for sending a contact email.
/// </summary>
public static class ContactService
{
    /// <summary>
    /// Sends an email based on the received HTTP POST request sent to the contact/ endpoint.
    /// </summary>
    /// <param name="req">The HTTP request.</param>
    /// <param name="messages">The collection of email messages to send.</param>
    /// <param name="log">The logger.</param>
    /// <returns>The result.</returns>
    [FunctionName("ContactMe")]
    public static async Task<IActionResult> ContactMeAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "contact")] HttpRequest req,
        [SendGrid(ApiKey = "SENDGRID_API_KEY")] IAsyncCollector<SendGridMessage> messages,
        ILogger log)
    {
        if (req is null)
        {
            throw new ArgumentNullException(nameof(req));
        }

        if (messages is null)
        {
            throw new ArgumentNullException(nameof(messages));
        }

        if (log is null)
        {
            throw new ArgumentNullException(nameof(log));
        }

        log.LogInformation("C# HTTP trigger function processed a request.");

        using var reader = new StreamReader(req.Body);
        string requestBody = await reader.ReadToEndAsync().ConfigureAwait(false);
        Contact contact = JsonConvert.DeserializeObject<Contact>(requestBody);

        var sendGridMessage = new SendGridMessage();
        sendGridMessage.AddTo(Environment.GetEnvironmentVariable("MY_EMAIL_ADDRESS", EnvironmentVariableTarget.Process));
        sendGridMessage.AddContent("text/html", contact.Message);
        sendGridMessage.SetSubject($"Your portfolio received a message from {contact.Name} ({contact.Email})");
        sendGridMessage.SetFrom(Environment.GetEnvironmentVariable("MY_EMAIL_ADDRESS", EnvironmentVariableTarget.Process));
        await messages.AddAsync(sendGridMessage).ConfigureAwait(false);

        return new OkResult();
    }
}
