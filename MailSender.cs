using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MailKit.Security;

class MailKitSender
{
    public MailKitSender()
    {

    }
    public void sendMail(string sender, string pass, string receiver, string attachmentPath)
    {

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(sender.Substring(0, sender.IndexOf("@")), sender));
        message.To.Add(new MailboxAddress(receiver.Substring(0, receiver.IndexOf("@")), receiver));
        message.Subject = "ReportByCountry";

        var builder = new BodyBuilder();

        builder.TextBody = @"ReportByCountry";

        builder.Attachments.Add(attachmentPath);

        message.Body = builder.ToMessageBody();

        var client = new SmtpClient();

        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        client.Connect("smtp.abv.bg", 465, SecureSocketOptions.SslOnConnect);
        client.Authenticate(sender, pass);
        client.Send(message);
        client.Disconnect(true);

    }
}