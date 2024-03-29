﻿// Copyright © 2022-2023 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using Xunit.Abstractions;

namespace EgonsoftHU.Extensions.Logging.Serilog.Autofac.UnitTests
{
    public class AutofacLoggingTestFramework : AutofacLoggingTestFrameworkBase<AutofacLoggingTestFramework>
    {
        public AutofacLoggingTestFramework(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
        }
    }
}
