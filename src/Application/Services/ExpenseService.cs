using Application.DTOs.Expense.RequestModel;
using Application.DTOs.Expense.ResponseModel;
using Application.Interfaces;
using Application.Interfaces.Expenses;
using Application.Shared;
using Domain.Entities;
using Domains.Execptions;

namespace Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> CreateExpenseAsync(CreateExpenseRequest request)
        {
            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Amount = request.Amount,
                Category = request.Category,
                Date = DateTime.UtcNow,
                UserId = request.UserId,
                Notes = request.Notes,
            };

            await _unitOfWork.Expenses.AddAsync(expense);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Expense created successfully.",
                Success = true
            };
        }

        public async Task<BaseResponse> UpdateExpenseAsync(UpdateExpenseRequest request)
        {
            var expense = await _unitOfWork.Expenses.GetAsync(request.Id);
            if (expense == null)
                throw new NotFoundException("Expense not found.");

            expense.Title = request.Title;
            expense.Amount = request.Amount;
            expense.Category = request.Category;
            expense.LastModified = DateTime.UtcNow;
            expense.Notes = request.Notes;  

             await _unitOfWork.Expenses.UpdateAsync(expense);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Expense updated successfully.",
                Success = true
            };
        }

        public async Task<BaseResponse> DeleteExpenseAsync(Guid id)
        {
            var expense = await _unitOfWork.Expenses.GetAsync(id);
            if (expense == null)
                throw new NotFoundException("Expense not found.");

           await _unitOfWork.Expenses.DeleteAsync(expense);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Expense deleted successfully.",
                Success = true
            };
        }

        public async Task<ExpenseResponse?> GetExpenseByIdAsync(Guid id)
        {
            var expense = await _unitOfWork.Expenses.GetAsync(id);
            if (expense is null)
                throw new NotFoundException("Expense not found.");

            return new ExpenseResponse
            {
                Id = expense.Id,
                Title = expense.Title,
                Amount = expense.Amount,
                Category = expense.Category,
                Date = expense.CreatedAt,
                Notes = expense.Notes
            };
        }

        public async Task<BaseResponse<IEnumerable<ExpenseResponse>>> GetAllExpensesAsync(Guid userId)
        {
            var userExpenses = await _unitOfWork.Expenses.GetAllBySpecAsync(x => x.UserId == userId && !x.IsDeleted);

            if (!userExpenses.Any())
                return new BaseResponse<IEnumerable<ExpenseResponse>>("You have not created any expenses yet.", false, Enumerable.Empty<ExpenseResponse>());

            var expenses = userExpenses.Select(e => new ExpenseResponse
            {
                Id = e.Id,
                Title = e.Title,
                Amount = e.Amount,
                Category = e.Category,
                Date = e.CreatedAt,
                Notes = e.Notes
            });

            return new BaseResponse<IEnumerable<ExpenseResponse>>("Expenses retrieved successfully.", true, expenses);
        }


        public async Task<BaseResponse<IEnumerable<CategorySummaryResponse>>> GetTotalSpendingByCategoryAsync(Guid userId)
        {
            var userExpenses = await _unitOfWork.Expenses.GetAllBySpecAsync(x => x.UserId == userId && !x.IsDeleted);

            if (!userExpenses.Any())
                return new BaseResponse<IEnumerable<CategorySummaryResponse>>("No spending records found for this user.", false, Enumerable.Empty<CategorySummaryResponse>());

            var categorySummary = userExpenses
                .GroupBy(e => e.Category)
                .Select(g => new CategorySummaryResponse
                {
                    Category = g.Key.ToString(),
                    TotalAmount = g.Sum(e => e.Amount)
                });

            return new BaseResponse<IEnumerable<CategorySummaryResponse>>("Total spending by category retrieved successfully.", true, categorySummary);
        }


        public async Task<BaseResponse<IEnumerable<ExpenseResponse>>> FilterExpensesAsync(FilterExpenseRequest filter, Guid userId)
        {
            var userExpenses = await _unitOfWork.Expenses.GetAllBySpecAsync(x => x.UserId == userId && !x.IsDeleted);

            if (!userExpenses.Any())
                return new BaseResponse<IEnumerable<ExpenseResponse>>("No expenses found for this user.", false, Enumerable.Empty<ExpenseResponse>());

            if (!string.IsNullOrEmpty(filter.Category))
                userExpenses = userExpenses.Where(e => e.Category.ToString().Equals(filter.Category, StringComparison.OrdinalIgnoreCase));

            if (filter.StartDate.HasValue)
                userExpenses = userExpenses.Where(e => e.CreatedAt >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                userExpenses = userExpenses.Where(e => e.CreatedAt <= filter.EndDate.Value);

            var filtered = userExpenses.Select(e => new ExpenseResponse
            {
                Id = e.Id,
                Title = e.Title,
                Amount = e.Amount,
                Category = e.Category,
                Notes = e.Notes,
                Date = e.CreatedAt
            });

            if (!filtered.Any())
                return new BaseResponse<IEnumerable<ExpenseResponse>>("No expenses match the provided filter criteria.", false, Enumerable.Empty<ExpenseResponse>());

            return new BaseResponse<IEnumerable<ExpenseResponse>>("Filtered expenses retrieved successfully.", true, filtered);
        }
        public async Task<BaseResponse<IEnumerable<SpendingTrendResponse>>> GetSpendingTrendAsync(Guid userId)
        {
            var userExpenses = await _unitOfWork.Expenses.GetAllBySpecAsync(x => x.UserId == userId && !x.IsDeleted);

            if (!userExpenses.Any())
                return new BaseResponse<IEnumerable<SpendingTrendResponse>>("No spending data available for this user.", false, Enumerable.Empty<SpendingTrendResponse>());

            var trend = userExpenses
                .GroupBy(e => e.CreatedAt.ToString("yyyy-MM"))
                .Select(g => new SpendingTrendResponse
                {
                    Month = g.Key,
                    TotalSpent = g.Sum(e => e.Amount)
                })
                .OrderBy(x => x.Month);

            return new BaseResponse<IEnumerable<SpendingTrendResponse>>("Spending trend retrieved successfully.", true, trend);
        }

    }
}
