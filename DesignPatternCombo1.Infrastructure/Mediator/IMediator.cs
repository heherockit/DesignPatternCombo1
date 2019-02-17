using DesignPatternCombo1.Infrastructure.CommandQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternCombo1.Infrastructure.Mediator
{
    public interface IMediator
    {
        Task<TResult> SendAsync<TResult>(IQuery<TResult> query);
        Task<ICommandResult> SendAsync(ICommand command);
    }
}
