// Copyright © 2022-2023 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using System;
using System.Collections.Generic;
using System.Reflection;

using Autofac;
using Autofac.Core.Registration;

using FluentAssertions;

using Serilog;

using Xunit;
using Xunit.Abstractions;

namespace EgonsoftHU.Extensions.Logging.Serilog.Autofac.UnitTests
{
    public abstract class LoggerInjectionUnitTestBase<T> : UnitTest<T>
        where T : LoggerInjectionUnitTestBase<T>
    {
        public LoggerInjectionUnitTestBase(ILifetimeScope lifetimeScope, ITestOutputHelper output, LoggingFixture<T> fixture)
            : base(lifetimeScope, output, fixture)
        {
            Logger.Here().Verbose("UnitTest class created");
        }

        [Fact]
        public void InTestServiceInjectedLoggerShouldHaveSameSourceContextAsManuallyInitializedLogger()
        {
            Logger.Here().Verbose("Running test");

            // Arrange
            Log.Logger = new LoggerConfiguration().WriteTo.Sink<TestLogEventSink>().CreateLogger();
            TestService service = Scope.Resolve<TestService>();

            int expectedCount = 2;
            string expectedValue = typeof(TestService).GetTypeInfo().FullName!;

            // Act
            service.LoggerInjected.Information(String.Empty);
            service.LoggerManuallyInitialized.Information(String.Empty);

            IReadOnlyList<string> sut = TestLogEventSink.SourceContexts;

            // Assert
            sut.Should().HaveCount(expectedCount, "because both TestService's loggers were called exactly once and TestLogEventSink is used only by them.");
            sut.Should().AllBe(expectedValue, "because both TestService's loggers were created the same way.");
        }

        [Fact]
        public void ShouldNotResolveIfKeyFilterAttributeApplied()
        {
            Logger.Here().Verbose("Running test");

            // Arrange
            TestService service = Scope.Resolve<TestService>();

            // Act
            ILogger? sut = service.KeyedLogger;

            // Assert
            sut.Should().BeNull();
        }

        [Fact]
        public void ResolveILoggerDirectlyShouldFail()
        {
            Logger.Here().Verbose("Running test");

            // Arrange

            // Act
            Func<ILogger> sut = () => { return Scope.Resolve<ILogger>(); };

            // Assert
            sut.Should().Throw<ComponentNotRegisteredException>("because ILogger service type is not registered");
        }

        protected override void Dispose(bool isDisposing)
        {
            if (!IsDisposed)
            {
                Log.CloseAndFlush();

                base.Dispose(isDisposing);
            }
        }
    }
}
