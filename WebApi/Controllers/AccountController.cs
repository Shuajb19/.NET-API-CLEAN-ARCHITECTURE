using Application.Commands;
using Application.Dto;
using Application.Handlers;
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

        public AccountController(CreditAccountHandler creditAccountHandler)
        {
            _creditAccountHandler = creditAccountHandler;
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

    }
}
