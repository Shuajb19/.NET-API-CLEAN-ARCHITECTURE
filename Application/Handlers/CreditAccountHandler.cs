using Application.Commands;
using Application.Contracts;

public class CreditAccountHandler
{
    private readonly IAccountRepository _accountRepository;

    public CreditAccountHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task Handle(CreditAccountCommand command)
    {
        var account = await _accountRepository.GetByUserIdAsync(command.UserId);

        if (account == null)
        {
            throw new InvalidOperationException("Account not found for the given user.");
        }

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

        await _accountRepository.UpdateAsync(account);
    }
}
