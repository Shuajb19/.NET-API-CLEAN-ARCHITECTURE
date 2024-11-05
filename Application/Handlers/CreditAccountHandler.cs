using Application.Commands;
using Application.Contracts;
using Domain.Entities;

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

        if (account == null)
        {
            throw new InvalidOperationException("Account not found for the given user.");
        }

        var transaction = new Transaction
        {
            AccountId = account.Id,
            Amount = command.CreditAccountDto.Amount,
            Date = command.CreditAccountDto.TransactionDate,
            TransactionType = command.CreditAccountDto.TransactionType,
            Description = command.CreditAccountDto.Description
        };

        // Check transaction type and apply logic accordingly
        if (command.CreditAccountDto.TransactionType.Equals("Credit", StringComparison.OrdinalIgnoreCase))
        {
            account.Balance += command.CreditAccountDto.Amount; // Add amount for credit
        }
        else if (command.CreditAccountDto.TransactionType.Equals("Debit", StringComparison.OrdinalIgnoreCase))
        {
            if (account.Balance >= command.CreditAccountDto.Amount)
            {
                account.Balance -= command.CreditAccountDto.Amount; // Subtract amount for debit
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds for debit transaction.");
            }
        }
        else
        {
            throw new ArgumentException("Invalid transaction type. Use 'Credit' or 'Debit'.");
        }

        await _transactionRepository.AddAsync(transaction);
        await _accountRepository.UpdateAsync(account);
    }
}
