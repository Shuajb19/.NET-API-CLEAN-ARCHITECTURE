using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(int id);
        Task<Account> GetByUserIdAsync(int userId); // Fetch by user ID
        Task UpdateAsync(Account account);
    }
}
