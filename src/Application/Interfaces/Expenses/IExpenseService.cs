
using Application.DTOs.Expense.RequestModel;
using Application.DTOs.Expense.ResponseModel;
using Application.Shared;

namespace Application.Interfaces.Expenses
{
    public interface IExpenseService
    {
        Task<BaseResponse> CreateExpenseAsync(CreateExpenseRequest request);
        Task<BaseResponse> UpdateExpenseAsync(UpdateExpenseRequest request);
        Task<BaseResponse> DeleteExpenseAsync(Guid id);
        Task<ExpenseResponse?> GetExpenseByIdAsync(Guid id);
        Task<BaseResponse<IEnumerable<ExpenseResponse>>> GetAllExpensesAsync(Guid userId);
        Task<BaseResponse<IEnumerable<CategorySummaryResponse>>> GetTotalSpendingByCategoryAsync(Guid userId);
        Task<BaseResponse<IEnumerable<ExpenseResponse>>> FilterExpensesAsync(FilterExpenseRequest filter, Guid userId);
        Task<BaseResponse<IEnumerable<SpendingTrendResponse>>> GetSpendingTrendAsync(Guid userId);
    }
}
