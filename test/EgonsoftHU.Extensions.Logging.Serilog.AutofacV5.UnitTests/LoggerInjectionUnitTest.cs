﻿// Copyright © 2022-2023 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using Autofac;

using Xunit.Abstractions;

namespace EgonsoftHU.Extensions.Logging.Serilog.Autofac.UnitTests
{
    public class LoggerInjectionUnitTest : LoggerInjectionUnitTestBase<LoggerInjectionUnitTest>
    {
        public LoggerInjectionUnitTest(ILifetimeScope lifetimeScope, ITestOutputHelper output, LoggingFixture<LoggerInjectionUnitTest> fixture)
            : base(lifetimeScope, output, fixture)
        {
        }
    }
}
