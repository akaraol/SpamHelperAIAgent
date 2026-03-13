using System;
using System.Collections.Generic;
using System.Text;

namespace Email_Cleaner_Agent.Models;

internal class EmailModel
{
    public required string Id { get; set; }
    public required string Sender { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}
