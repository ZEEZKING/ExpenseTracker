using Domain.Enum;

namespace Application.DTOs.Expense.ResponseModel
{
    public record ExpenseResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Amount { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
    }
}
