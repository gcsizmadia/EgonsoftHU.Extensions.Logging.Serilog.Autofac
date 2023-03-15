// Copyright © 2022-2023 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using System.Collections.Generic;

using Serilog.Core;
using Serilog.Events;

namespace EgonsoftHU.Extensions.Logging.Serilog.Autofac.UnitTests
{
    internal class TestLogEventSink : ILogEventSink
    {
        private const string SourceContextPropertyName = "SourceContext";

        private static readonly List<string> sourceContexts = new();

        public static IReadOnlyList<string> SourceContexts => sourceContexts.AsReadOnly();

        public void Emit(LogEvent logEvent)
        {
            if (logEvent.Properties.TryGetValue(SourceContextPropertyName, out LogEventPropertyValue? sourceContext))
            {
                sourceContexts.Add(sourceContext.ToString().Trim('"'));
            }
        }
    }
}
