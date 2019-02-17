using DesignPatternCombo1.Infrastructure.CommandQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternCombo1.Infrastructure.Decorators
{
    public class LogDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery: IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratee;

        public LogDecorator(IQueryHandler<TQuery, TResult> decoratee)
        {
            _decoratee = decoratee;
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            Console.WriteLine(query.ToString());
            return await _decoratee.HandleAsync(query);
        }
    }
}
