// Copyright © 2022 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using System;
using System.Collections.Generic;
using System.Reflection;

using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Core.Resolving.Pipeline;
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
        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {
            registration.PipelineBuilding += (sender, e) =>
            {
                e.Use(
                    "Serilog.ILogger instance injection middleware",
                    PipelinePhase.ParameterSelection,
                    MiddlewareInsertionMode.StartOfPhase,
                    (context, next) =>
                    {
                        var parameters = new List<Parameter>(context.Parameters) { loggerParameter };

                        context.ChangeParameters(parameters);

                        next.Invoke(context);
                    }
                );
            };
        }
    }
}
