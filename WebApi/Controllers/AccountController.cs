using Application.Commands;
using Application.Dto;
using Application.Handlers;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly CreditAccountHandler _creditAccountHandler;
        private readonly IMediator _mediator;
        private readonly AppDbContext _appDbcontext;

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
            var accountId = GetCurrentAccountId();
            var command = new CreditAccountCommand
            {
                UserId = GetCurrentUserId(), // Extract user ID as you did before
                CreditAccountDto = new CreditAccountDto
                {
                    AccountId = accountId, // Set the AccountId from claims
                    Amount = creditAccountDto.Amount,
                    TransactionDate = creditAccountDto.TransactionDate,
                    TransactionType = creditAccountDto.TransactionType,
                    Description = creditAccountDto.Description
                }
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
                throw new Exception("Account ID claim not found.");
            }

            if (int.TryParse(accountIdClaim.Value, out int accountId))
            {
                return accountId;
            }
            else
            {
                throw new Exception("Invalid Account ID claim value.");
            }
        }

    }
}
