using Application.Contracts;
using Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetTransactionHistoryByAccountQueryHandler : IRequestHandler<GetTransactionHistoryByAccountQuery, List<TransactionDto>>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionHistoryByAccountQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<TransactionDto>> Handle(GetTransactionHistoryByAccountQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.GetTransactionsByAccountIdAsync(request.AccountId);

            if (transactions == null || !transactions.Any())
            {
                throw new Exception("No transactions found for the specified account.");
            }

            return transactions.Select(t => new TransactionDto
            {
                TransactionId = t.Id,
                Amount = t.Amount,
                TransactionType = t.TransactionType,
                TransactionDate = t.Date
            }).ToList();
        }
    }

}
