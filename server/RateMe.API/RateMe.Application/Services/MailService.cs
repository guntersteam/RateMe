using RateMe.Application.Interfaces.Services;

namespace RateMe.Application.Services;

public class MailService : IMailService
{
   public Task SendActivationOnMail(string userEmail, string activationLink)
   {
      throw new NotImplementedException();
   }
}