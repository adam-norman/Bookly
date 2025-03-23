using Application.Abstraction.Email;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        public EmailService(ILogger<EmailService> logger) 
        {
            _logger = logger;
        }
        public Task SendEmailAsync(Domain.Entities.Users.Email to, string subject, string body)
        {
            _logger.LogInformation($"Sending email to {to.Value} with subject {subject} and body {body}");
            return Task.CompletedTask;
        }
    }
}
