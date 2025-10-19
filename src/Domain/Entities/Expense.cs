using Domain.Common;
using Domain.Enum;

namespace Domain.Entities
{
    public class Expense : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string Title { get; set; } = default!;
        public decimal Amount { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
    }
}
