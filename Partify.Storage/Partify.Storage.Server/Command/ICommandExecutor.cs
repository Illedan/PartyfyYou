using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Command
{
    /// <summary>
    /// Represents a class that is capable of executing a command.
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// Executes the given <paramref name="command"/>.
        /// </summary>
        /// <typeparam name="TCommand">The type of command to be executed.</typeparam>
        /// <param name="command">The command to be executed.</param>
        /// <returns><see cref="Task"/>.</returns>
        Task ExecuteAsync<TCommand>(TCommand command);
    }
}
