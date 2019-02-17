using DesignPatternCombo1.Infrastructure.CommandQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignPatternCombo1.Web.Features.Values
{
    public sealed class ExactQuery : IQuery<string>
    {
        public string Keyword { get; set; }

        public sealed class ExactQueryHandler : IQueryHandler<ExactQuery, string>
        {
            public async Task<string> HandleAsync(ExactQuery query)
            {
                return await Task.Run(() => query.Keyword + "Exact");
            }
        }
    }
}
