using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetAccountBalanceQuery : IRequest<decimal>
    {
        public int UserId { get; set; }

        public GetAccountBalanceQuery(int userId)
        {
            UserId = userId;
        }
    }

}
