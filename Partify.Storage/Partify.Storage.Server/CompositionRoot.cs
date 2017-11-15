using LightInject;
using Partify.Storage.Server.Mode;
using Partify.Storage.Server.CQRS;
using System.Data;
using System.Data.SqlClient;
using Partify.Storage.Server.Configuration;

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
                .Register<IConfiguration,PartifyConfiguration>(new PerContainerLifetime())
                .Register<IDbConnection>(factory => CreateMySqlConnection(factory));
        }

        private SqlConnection CreateMySqlConnection(IServiceFactory factory)
        {
            var partifyConfiguration = factory.GetInstance<IConfiguration>();
            var connectionString = partifyConfiguration.ConnectionString;
            var connection = new SqlConnection(connectionString);
            return connection;

        }
    }
}
