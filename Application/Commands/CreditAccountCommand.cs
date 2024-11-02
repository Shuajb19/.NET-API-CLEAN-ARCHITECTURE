using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreditAccountCommand
    {
        public int UserId { get; set; }
        public CreditAccountDto CreditAccountDto { get; set; } // Use DTO here
    }
}
