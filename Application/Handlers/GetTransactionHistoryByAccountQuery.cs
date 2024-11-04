using Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetTransactionHistoryByAccountQuery : IRequest<List<TransactionDto>>
    {
        public int AccountId { get; set; }

        public GetTransactionHistoryByAccountQuery(int accountId)
        {
            AccountId = accountId;
        }
    }

}
