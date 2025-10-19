using Domain.Common;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
