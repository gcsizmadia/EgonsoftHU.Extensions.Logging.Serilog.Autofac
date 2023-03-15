// Copyright © 2022 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using System;
using System.Collections.Generic;
using System.Reflection;

using Autofac.Core;
using Autofac.Features.AttributeFilters;

using Serilog;

using Module = Autofac.Module;

namespace EgonsoftHU.Extensions.Logging.Serilog.Autofac
{
    /// <summary>
    /// A dependency module (derived from <see cref="Module"/>) that enables injecting
    /// a contextual logger (<see cref="ILogger"/>) using <see cref="ILogger.ForContext(Type)"/>.
    /// </summary>
    public class DependencyModule : Module
    {
        private static readonly ResolvedParameter loggerParameter =
            new(
                (parameter, context) => typeof(ILogger) == parameter.ParameterType && parameter.GetCustomAttribute<KeyFilterAttribute>() is null,
                (parameter, context) => Log.Logger.ForContext(parameter.Member.DeclaringType ?? typeof(object))
            );

        /// <inheritdoc/>
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            registration.Preparing +=
                (sender, e) =>
                {
                    e.Parameters = new List<Parameter>(e.Parameters) { loggerParameter };
                };
        }
    }
}
