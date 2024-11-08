﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
    }
}
