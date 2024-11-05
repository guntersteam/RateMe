namespace RateMe.Application.Interfaces.Services;

public interface IMailService
{
   Task SendActivationOnMail(string userEmail,string activationLink);
}