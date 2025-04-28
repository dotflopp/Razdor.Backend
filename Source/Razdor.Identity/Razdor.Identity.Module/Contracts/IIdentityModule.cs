using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mediator;

namespace Razdor.Identity.Module.Contracts
{
    public interface IIdentityModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(IIdentityCommand<TResult> command);
        Task ExecuteCommandAsync(IIdentityCommand command);
        Task<TResult> ExecuteQueryAsync<TResult>(IIdentityQuery<TResult> query);
    }
}
