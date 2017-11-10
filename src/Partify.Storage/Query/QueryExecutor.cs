using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Query
{
    /// <summary>
    /// An <see cref="IQueryExecutor"/> that is capable of executing a query.
    /// </summary>
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IServiceFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryExecutor"/> class.
        /// </summary>
        /// <param name="factory">The <see cref="IServiceFactory"/> used to resolve the 
        /// <see cref="IQueryHandler{TQuery,TResult}"/> to be executed.</param>
        public QueryExecutor(IServiceFactory factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// Executes the given <paramref name="query"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of result returned by the query.</typeparam>
        /// <param name="query">The query to be executed.</param>
        /// <returns>The result from the query.</returns>
        public async Task<TResult> HandleAsync<TResult>(IQuery<TResult> query)
        {
            Type queryType = query.GetType();
            Type queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));
            dynamic queryHandler = factory.GetInstance(queryHandlerType);

            return await queryHandler.HandleAsync((dynamic)query);
        }
    }
}
