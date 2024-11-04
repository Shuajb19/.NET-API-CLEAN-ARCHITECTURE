using Application.Commands;
using Application.Dto;
using Application.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly CreditAccountHandler _creditAccountHandler;
        private readonly IMediator _mediator;

        public AccountController(CreditAccountHandler creditAccountHandler, IMediator mediator)
        {
            _creditAccountHandler = creditAccountHandler;
            _mediator = mediator;
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetAccountBalance()
        {
            var userId = GetCurrentUserId(); // Helper method to get the current user's ID
            var balance = await _mediator.Send(new GetAccountBalanceQuery(userId));
            return Ok(new { Balance = balance });
        }

        [HttpPost("credit")]
        public async Task<IActionResult> CreditAccount([FromBody] CreditAccountDto creditAccountDto)
        {
            var userId = GetCurrentUserId(); // Extract user ID
            var command = new CreditAccountCommand
            {
                UserId = userId,
                CreditAccountDto = creditAccountDto // Populate DTO
            };

            await _creditAccountHandler.Handle(command);
            return Ok();
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactionHistory()
        {
            var accountId = GetCurrentAccountId();
            var transactions = await _mediator.Send(new GetTransactionHistoryByAccountQuery(accountId));
            return Ok(transactions);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User ID claim not found.");
            }

            if (int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            else
            {
                throw new Exception("Invalid User ID claim value.");
            }
        }

        private int GetCurrentAccountId()
        {
            var accountIdClaim = User.Claims.FirstOrDefault(c => c.Type == "AccountId");

            if (accountIdClaim == null)
            {
                throw new Exception("User ID claim not found.");
            }

            if (int.TryParse(accountIdClaim.Value, out int accountId))
            {
                return accountId;
            }
            else
            {
                throw new Exception("Invalid User ID claim value.");
            }
        }

    }
}
