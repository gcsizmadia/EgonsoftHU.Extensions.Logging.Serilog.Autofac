// Copyright © 2022-2023 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using Autofac.Features.AttributeFilters;

using Serilog;

namespace EgonsoftHU.Extensions.Logging.Serilog.Autofac.UnitTests
{
    public class TestService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestService"/> class.
        /// </summary>
        /// <param name="logger">A contextual logger injected by Autofac by calling <c>Log.Logger.ForContext(typeof(TestService))</c>.</param>
        public TestService(ILogger logger, [KeyFilter("Custom")] ILogger? keyedLogger = null)
        {
            LoggerInjected = logger;
            KeyedLogger = keyedLogger;
        }

        public ILogger LoggerInjected { get; }

        public ILogger? KeyedLogger { get; }

        public ILogger LoggerManuallyInitialized { get; } = Log.Logger.ForContext<TestService>();
    }
}
