using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

//This extends ASP.NET Identity to link users to transactions.
public class ApplicationUser : IdentityUser
{
    public ICollection<Transaction> Transactions { get; set; }
}
