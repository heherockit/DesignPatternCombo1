using DesignPatternCombo1.Infrastructure.CommandQuery;
using DesignPatternCombo1.Web.Features.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DesignPatternCombo1.Web.Features.Values.ValueQuery;

namespace DesignPatternCombo1.Web.Decorators
{
    public class AppendToKeywordDecorator : IQueryHandler<ValueQuery, IEnumerable<string>>
    {
        private readonly IQueryHandler<ValueQuery, IEnumerable<string>> _decoratee;

        public AppendToKeywordDecorator(IQueryHandler<ValueQuery, IEnumerable<string>> decoratee)
        {
            _decoratee = decoratee;
        }

        public async Task<IEnumerable<string>> HandleAsync(ValueQuery query)
        {
            query.Keyword += "Decorated";
            return await _decoratee.HandleAsync(query);
        }
    }
}
