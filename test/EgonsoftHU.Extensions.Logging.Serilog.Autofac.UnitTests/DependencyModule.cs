// Copyright © 2022 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using Autofac;

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
                .InstancePerTest();
        }
    }
}
