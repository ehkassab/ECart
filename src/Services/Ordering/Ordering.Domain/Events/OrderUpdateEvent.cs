﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Events
{ 
    public record OrderUpdateEvent(Order Order) : IDomainEvents;
}
