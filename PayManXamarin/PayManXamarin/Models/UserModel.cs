using System;
using System.Collections.Generic;
using System.Text;

namespace PayManXamarin.Models
{
    public class UserModel
    {
    public Guid Id { get; set; }
    public String UserName { get; set; }
    public double Frikort { get; set; }
    public double Hovedkort { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
}
