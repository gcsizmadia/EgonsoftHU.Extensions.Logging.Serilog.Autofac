// Copyright © 2022-2023 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using Autofac;
using Autofac.Features.AttributeFilters;

using Xunit.Frameworks.Autofac;

namespace EgonsoftHU.Extensions.Logging.Serilog.Autofac.UnitTests
{
    public class DependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<TestService>()
                .AsSelf()
                .InstancePerTest()
                .WithAttributeFiltering();
        }
    }
}
