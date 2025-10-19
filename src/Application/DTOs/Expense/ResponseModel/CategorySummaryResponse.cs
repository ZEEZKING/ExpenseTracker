using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Expense.ResponseModel
{
    public class CategorySummaryResponse
    {
        public string Category { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }
}
