using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataHub.Api.Services
{
    public interface IEmailService
    {
        Task<bool> SendMail(string to, string toName, string subject, string html);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly MailjetClient _client;

        private readonly string _emailFrom;
        private readonly string _emailFromName;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        private readonly string _emailToOverride;

        public EmailService(IConfiguration config, ILoggerFactory loggerFactory)
        {
            _config = config;
            _logger = loggerFactory.CreateLogger<EmailService>();

            _emailFrom = _config["SmtpSettings:EmailFrom"];
            _emailFromName = _config["SmtpSettings:EmailFromName"];

            _smtpUser = _config["SmtpSettings:SmtpUser"];
            _smtpPass = _config["SmtpSettings:SmtpPass"];

            if (string.Equals(_config["SmtpSettings:EnableGlobalOverride"], "true", StringComparison.InvariantCultureIgnoreCase))
            {

                if (!string.IsNullOrWhiteSpace(_config["SmtpSettings:GlobalEmailToOverride"]))
                {
                    _emailToOverride = _config["SmtpSettings:GlobalEmailToOverride"];
                }
                else
                {
                    _logger.LogWarning("Global override for SMTP settings was enabled but no email was found in GlobalEmailToOverride. Using override@example.com instead.");
                    _emailToOverride = "override@example.com";
                }
            }

            _client = new MailjetClient(_smtpUser, _smtpPass);
        }

        public async Task<bool> SendMail(string to, string toName, string subject, string html)
        {
            var emailToJArray = _emailToOverride == null
                ? new JArray
                {
                    new JObject
                    {
                        {"Email", to},
                        {"Name", toName}
                    }
                }
                : new JArray
                {
                    new JObject
                    {
                        {"Email", _emailToOverride},
                        {"Name", $"Data Hub Override ({to})"}
                    }
                };

            MailjetRequest request = new MailjetRequest
            {
                Resource = SendV31.Resource,
            }
            .Property(Send.Messages, new JArray {
                new JObject {
                    { "From", new JObject{
                        {"Email", _emailFrom},
                        {"Name", _emailFromName}
                    }
                    },
                    { "To", emailToJArray },
                    { "Subject", subject },
                    { "HTMLPart", html },
                    { "CustomID", "AppGettingStartedTest" }
                }
            });

            MailjetResponse response = await _client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                _logger.LogInformation(response.GetData().ToString());
            }
            else
            {
                _logger.LogInformation(string.Format("StatusCode: {0}\n", response.StatusCode));
                _logger.LogError(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                _logger.LogInformation(response.GetData().ToString());
                _logger.LogError(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
                return false;
            }

            return true;
        }
    }
}
