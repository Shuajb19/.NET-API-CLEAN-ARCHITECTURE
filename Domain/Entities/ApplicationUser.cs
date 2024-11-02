
using System.Security.Principal;

namespace Domain.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
