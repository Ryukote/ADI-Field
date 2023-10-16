using Domain.Common;

namespace Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsUsingWhatsapp { get; set; }
    public bool IsUsingDiscord { get; set; }
    public string DiscordName { get; set; }
    public int MyProperty { get; set; }
}
