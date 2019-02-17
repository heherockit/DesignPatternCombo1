using DesignPatternCombo1.Infrastructure.CommandQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignPatternCombo1.Web.Features.Values
{
    public sealed class ValueQuery : IQuery<IEnumerable<string>>
    {
        public string Keyword { get; set; }

        public sealed class ValueQueryHandler : IQueryHandler<ValueQuery, IEnumerable<string>>
        {
            public async Task<IEnumerable<string>> HandleAsync(ValueQuery query)
            {
                return await Task.Run(() => new string[] { "a", "b", "c", query.Keyword });
            }
        }
    }
}
