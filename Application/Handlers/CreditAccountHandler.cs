using Application.Commands;
using Application.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreditAccountHandler
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CreditAccountHandler(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task Handle(CreditAccountCommand command)
        {
            var account = await _accountRepository.GetByUserIdAsync(command.UserId);
            if (account == null) throw new Exception("Account not found.");

            account.Balance += command.CreditAccountDto.Amount;

            var transaction = new Transaction
            {
                Amount = command.CreditAccountDto.Amount,
                Date = DateTime.UtcNow,
                Description = "Credit operation",
                AccountId = account.Id
            };

            await _accountRepository.UpdateAsync(account);
            await _transactionRepository.AddAsync(transaction);
        }
    }

}
