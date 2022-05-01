// Copyright © 2022 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using System;
using System.Linq;

using Autofac;
using Autofac.Core;

using EgonsoftHU.Extensions.Bcl;

using Serilog;

namespace EgonsoftHU.Extensions.Logging.Serilog.Autofac
{
    /// <summary>
    /// A dependency module (derived from <see cref="Module"/>) that enables injecting
    /// a contextual logger (<see cref="ILogger"/>) using <see cref="ILogger.ForContext(Type)"/>.
    /// </summary>
    public class DependencyModule : Module
    {
        private static readonly ResolvedParameter loggerParameter =
            new ResolvedParameter(
                (parameter, context) => typeof(ILogger) == parameter.ParameterType,
                (parameter, context) => Log.Logger.ForContext(parameter.Member.DeclaringType)
            );

        /// <inheritdoc/>
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            registration.Preparing +=
                (sender, e) =>
                {
                    e.Parameters = e.Parameters.Union(loggerParameter.AsEnumerable()).ToList();
                };
        }
    }
}
