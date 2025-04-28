using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Razdor.Identity.Infrastructure;

public record IdentityModuleOptions(
    DateTime AccessTokenStartDate,
    byte[] AccessTokenSecurityKey,
    string SqlConnectionString
);
