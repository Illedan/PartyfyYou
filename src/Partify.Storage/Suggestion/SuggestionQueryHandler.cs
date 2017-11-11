using Partify.Storage.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Suggestion
{
    public class SuggestionQueryHandler : IQueryHandler<SuggestionQuery, SuggestionResult>
    {
        public Task<SuggestionResult> HandleAsync(SuggestionQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
