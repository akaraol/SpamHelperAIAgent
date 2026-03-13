using Email_Cleaner_Agent.Models;
using MailKit;
using MailKit.Net.Imap;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Email_Cleaner_Agent.Services;

internal interface IEmailService
{
    public ICollection<EmailModel> FetchMails(ImapClient client);

    public ICollection<Guid> MoveMails(IEnumerable<string> id);

    public ICollection<Guid> Delete(IEnumerable<string> id);
}

public class EmailService(IConfiguration config) : IEmailService
{
    public ICollection<Guid> Delete(IEnumerable<string> id)
    {
        throw new NotImplementedException();
    }

    public ICollection<Guid> MoveMails(IEnumerable<string> id)
    {
        throw new NotImplementedException();
    }

    ICollection<EmailModel> IEmailService.FetchMails(ImapClient client)
    {
        var host = config["EmailSettings:ImapServer"];
        var port = int.Parse(config["EmailSettings:Port"]!);
        var email = config["EmailSettings:Email"];
        var password = config["EmailSettings:Password"];

        client.Connect(host, port, true);
        client.Authenticate(email, password);

        client.Inbox.Open(FolderAccess.ReadOnly);


        int mailcount = client.Inbox.Count;

        return Enumerable.Range(0, mailcount).Select( p =>
        {
            var text = client.Inbox.GetMessage(p);
            return new EmailModel
            {
                Id = text.MessageId!,
                Sender = text.From.ToString(),
                Subject = text.Subject!,
                Body = text.TextBody!
            };
        }).ToList();

    }
}
