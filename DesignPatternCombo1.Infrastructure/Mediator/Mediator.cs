using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DesignPatternCombo1.Infrastructure.CommandQuery;
using SimpleInjector;

namespace DesignPatternCombo1.Infrastructure.Mediator
{
    public class Mediator : IMediator
    {
        private readonly Container _container;

        public Mediator(Container container)
        {
            _container = container;
        }

        public async Task<TResult> SendAsync<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();

            var type = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));

            var instance = _container.GetInstance(type);

            if (instance == null)

            {

                throw new TypeLoadException(

                    $"No query handler type found for query type: {queryType.FullName}");

            }

            return await (Task<TResult>)type.GetMethod("HandleAsync").Invoke(instance, new object[] { query });
        }

        public async Task<ICommandResult> SendAsync(ICommand command)
        {
            var commandType = command.GetType();

            var type = typeof(ICommandHandler<>).MakeGenericType(commandType);

            var instance = _container.GetInstance(type);

            if (instance == null)

            {

                throw new TypeLoadException(

                    $"No command handler type found for command type: {commandType.FullName}");

            }

            return await (Task<ICommandResult>)type.GetMethod("HandleAsync").Invoke(instance, new object[] { command });
        }
    }
}
