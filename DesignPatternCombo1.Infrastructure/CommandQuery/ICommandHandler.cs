using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternCombo1.Infrastructure.CommandQuery
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<ICommandResult> HandleAsync(TCommand command);
    }
}
