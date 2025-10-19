

namespace Application.DTOs.Expense.ResponseModel
{
    public class SpendingTrendResponse
    {
        public string Month { get; set; } = string.Empty;
        public decimal TotalSpent { get; set; }
    }
}
