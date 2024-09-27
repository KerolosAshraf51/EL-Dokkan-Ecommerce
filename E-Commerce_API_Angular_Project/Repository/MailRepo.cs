using E_Commerce_API_Angular_Project.Interfaces;
using MailKit.Security;
using System;
using MailKit.Net.Smtp;
using MimeKit;



namespace E_Commerce_API_Angular_Project.Repository
{
    public class MailRepo : IMailRepo
    {
        public async void SendEmail(string toAddress, string subject, string body)

        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("EL-DOKAN", "eldokanonlinestore@hotmail.com"));
            message.To.Add(new MailboxAddress("", toAddress));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };  
              
            using (var client = new SmtpClient())
            {
                await  client.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("eldokanonlinestore@hotmail.com", "dokan12345");
                client.Send(message);
                client.Disconnect(true);
            }
        }


     
}
}
