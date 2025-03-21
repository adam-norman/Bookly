using Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users
{
    public static class UserErrors
    {
        public static Error NotFound = new(
            "User.Found",
            "The User with the specified identifier was not found");
    }
}
