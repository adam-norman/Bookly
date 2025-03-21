namespace Application.Abstraction.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(Domain.Entities.Users.Email to, string subject, string body);
    }
}
