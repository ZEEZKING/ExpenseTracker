using Application.DTOs.Expense.RequestModel;
using Application.Interfaces.Expenses;
using Domains.Execptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseRequest request)
        {
            try
            {
                request.UserId = GetUserId();
                var result = await _expenseService.CreateExpenseAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateExpense([FromBody] UpdateExpenseRequest request)
        {
            try
            {
                var result = await _expenseService.UpdateExpenseAsync(request);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            try
            {
                var result = await _expenseService.DeleteExpenseAsync(id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetExpenseById(Guid id)
        {
            try
            {
                var result = await _expenseService.GetExpenseByIdAsync(id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllExpenses()
        {
            try
            {
                
                var result = await _expenseService.GetAllExpensesAsync(GetUserId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("summary/category")]
        public async Task<IActionResult> GetTotalSpendingByCategory()
        {
            try
            {
                var result = await _expenseService.GetTotalSpendingByCategoryAsync(GetUserId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterExpenses([FromBody] FilterExpenseRequest request)
        {
            try
            {
                var result = await _expenseService.FilterExpensesAsync(request, GetUserId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("trend")]
        public async Task<IActionResult> GetSpendingTrend()
        {
            try
            {
                var result = await _expenseService.GetSpendingTrendAsync(GetUserId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("User ID claim not found in token.");

            return Guid.Parse(userIdClaim);
        }
    }
}
