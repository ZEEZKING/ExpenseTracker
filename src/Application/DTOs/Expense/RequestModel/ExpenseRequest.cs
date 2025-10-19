using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs.Expense.RequestModel
{
    public class CreateExpenseRequest
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
    }

    public class UpdateExpenseRequest
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public decimal Amount { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
    }
}
