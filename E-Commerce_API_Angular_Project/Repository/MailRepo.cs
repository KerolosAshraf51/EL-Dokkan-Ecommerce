using E_Commerce_API_Angular_Project.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;



namespace E_Commerce_API_Angular_Project.Repository
{
    public class MailRepo : IMailRepo
    {
        public async Task<string> SendEmail(string toAddress, string subject, string body)

        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("EL-DOKAN", "fernando.hackett@ethereal.email"));
            message.To.Add(new MailboxAddress("", toAddress));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("fernando.hackett@ethereal.email", "Pc38CeGeGABd116Wzz");
                    client.Send(message);
                    client.Disconnect(true);
                }

                return "1";

            }

            catch (Exception ex)
            {
                return ex.Message;
            }

        }




    }
}
