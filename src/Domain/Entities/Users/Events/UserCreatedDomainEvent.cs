﻿using Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users.Events
{
    public sealed record UserCreatedDomainEvent(Guid UserId):IDomainEvent;
}
