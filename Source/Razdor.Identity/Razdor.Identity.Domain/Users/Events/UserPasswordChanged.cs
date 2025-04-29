using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Razdor.Shared.Domain;

namespace Razdor.Identity.Domain.Users.Events
{
    public record UserPasswordChanged(
        UserAccount User,
        string? OldHashedPassword
    ): IDomainEvent;
}
