﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Command
{
    /// <summary>
    /// Represents a class that is capable of handling a <typeparamref name="TCommand"/>.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to handle.</typeparam>
    public interface ICommandHandler<in TCommand>
    {
        /// <summary>
        /// Handles the given <paramref name="command"/>.
        /// </summary>
        /// <param name="command">THe command to handle.</param>
        /// <returns><see cref="Task"/>.</returns>
        Task HandleAsync(TCommand command);
    }
}
