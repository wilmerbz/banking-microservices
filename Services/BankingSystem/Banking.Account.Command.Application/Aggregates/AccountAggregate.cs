using Banking.CQRS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Command.Application.Aggregates
{
    public class AccountAggregate : AggregateRoot
    {

        public bool Active { get; set; }

        public double Balance { get; set; }

        public AccountAggregate()
        {
        }
    }
}
