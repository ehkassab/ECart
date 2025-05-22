using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Exceptions
{
    public class DomainExceptions : Exception
    {
        public DomainExceptions(string exception) : base($"Domain Exception: \"{exception}\" throws from Domain Layer")
        {
        }
    }
}
