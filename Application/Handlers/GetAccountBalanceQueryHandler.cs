using Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQuery, decimal>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountBalanceQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<decimal> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByUserIdAsync(request.UserId);

            if (account == null)
            {
                throw new Exception("Account not found for the current user.");
            }

            return account.Balance;
        }
    }

}
