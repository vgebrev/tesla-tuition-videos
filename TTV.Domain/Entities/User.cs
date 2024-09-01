using System.Text;

namespace TTV.Domain.Entities;

public class User : BaseEntity<Guid>
{

    public class UserClaim(string type, string value)
    {
        public UserClaim() : this(string.Empty, string.Empty) { }
        public int Id { get; set; }
        public string ClaimType { get; set; } = type;
        public string ClaimValue { get; set; } = value;
    }

    public class UserLogin(string loginProvider, string providerKey, string providerDisplayName)
    {
        public UserLogin() : this(string.Empty, string.Empty, string.Empty) { }
        public Guid UserId { get; set; }
        public string LoginProvider { get; set; } = loginProvider;
        public string ProviderKey { get; set; } = providerKey;
        public string ProviderDisplayName { get; set; } = providerDisplayName;
    }

    public User()
    {
        Orders = [];
        OwnedLessons = [];
        Notifications = [];
    
        Claims = [];
        Logins = [];
    }

    private string GetName()
    {
        var result = "";
        var name = Claims.FirstOrDefault(c => c.ClaimType == "name" || c.ClaimType == "given_name")?.ClaimValue.Trim();
        if (string.IsNullOrEmpty(name))
        {
            name = Email;
        }
        if (string.IsNullOrEmpty(name))
        {
            name = UserName;
        }
        result = name;
        if (!string.IsNullOrEmpty(Email) && name != Email)
        {
            result += $" ({Email})";
        }
        return result;
    }

    public string Name => GetName();
    public string Provider => Logins.FirstOrDefault()?.ProviderDisplayName ?? "TTV Account";

    public string UserName { get; set; } = default!;
    public string? Email { get; set; }
    public virtual ICollection<Order> Orders { get; private set; }
    public virtual ICollection<Lesson> OwnedLessons { get; private set; }
    public virtual ICollection<Notification> Notifications { get; private set; }
    public virtual ICollection<UserClaim> Claims { get; set; }
    public virtual ICollection<UserLogin> Logins { get; set; }
}
