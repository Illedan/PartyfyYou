using LightInject;
using Partify.Storage.Server.Mode;
using Partify.Storage.Server.CQRS;
using System.Data;
using System;
using System.Data.SqlClient;

namespace Partify.Storage.Server
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry
                .RegisterQueryHandlers()
                .RegisterCommandHandlers()
                .Register<IModeService, ModeService>(new PerScopeLifetime())
                
                .Register<IDbConnection>(factory => CreateMySqlConnection(factory));
        }

        private SqlConnection CreateMySqlConnection(IServiceFactory factory)
        {
           //TODO: add string
            var connection = new SqlConnection("");
            connection.Open();
            return connection;
        }
    }
}
