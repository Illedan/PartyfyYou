using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Query
{
    public interface IQueryExecutor
    {
        /// <summary>
        /// Executes the given <paramref name="query"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of result returned by the query.</typeparam>
        /// <param name="query">The query to be executed.</param>
        /// <returns>The result from the query.</returns>
        Task<TResult> HandleAsync<TResult>(IQuery<TResult> query);
    }
}
