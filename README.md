# Egonsoft.HU Logging Extensions for Autofac

[![GitHub](https://img.shields.io/github/license/gcsizmadia/EgonsoftHU.Extensions.Logging.Serilog.Autofac?label=License)](https://opensource.org/licenses/MIT)
[![Nuget](https://img.shields.io/nuget/v/EgonsoftHU.Extensions.Logging.Serilog.Autofac?label=NuGet)](https://www.nuget.org/packages/EgonsoftHU.Extensions.Logging.Serilog.Autofac)
[![Nuget](https://img.shields.io/nuget/dt/EgonsoftHU.Extensions.Logging.Serilog.Autofac?label=Downloads)](https://www.nuget.org/packages/EgonsoftHU.Extensions.Logging.Serilog.Autofac)

A dependency module (derived from Autofac.Module) that enables injecting a contextual `Serilog` logger so that you can avoid calling `Log.Logger.ForContext<T>()` manually.

## Table of Contents
- [Introduction](#introduction)
- [Releases](#releases)
- [Summary](#summary)
- [Instructions](#instructions)
  - [Usage option #1 - Register the module explicitly](#usage-option-1---register-the-module-explicitly)
  - [Usage option #2 - Register the module implicitly by using `EgonsoftHU.Extensions.DependencyInjection.Autofac` nuget package](#usage-option-2---register-the-module-implicitly-by-using-egonsofthuextensionsdependencyinjectionautofac-nuget-package)

## Introduction

The motivation behind this project is to enable the constructor injection of a contextual Serilog logger.

So instead of initializing the private field manually:
```C#
using Serilog;

public class MyService
{
    private readonly ILogger logger = Log.Logger.ForContext<MyService>();

    public MyService()
    {
    }
}
```

We can simply inject the `ILogger` interface type and still get the same contextual logger instance:
```C#
using Microsoft.Extensions.Logging;

public class MyService
{
    private readonly ILogger logger;

    public MyService(ILogger logger)
    {
        this.logger = logger;
    }
}
```

## Releases

You can download the package from [nuget.org](https://www.nuget.org/).
- [EgonsoftHU.Extensions.Logging.Serilog.Autofac 4.0.0](https://www.nuget.org/packages/EgonsoftHU.Extensions.Logging.Serilog.Autofac/4.0.0)
- [EgonsoftHU.Extensions.Logging.Serilog.Autofac 5.0.0](https://www.nuget.org/packages/EgonsoftHU.Extensions.Logging.Serilog.Autofac/5.0.0)
- [EgonsoftHU.Extensions.Logging.Serilog.Autofac 6.0.0](https://www.nuget.org/packages/EgonsoftHU.Extensions.Logging.Serilog.Autofac/6.0.0)

**Please note:** Each package version reflects the major version of the referenced Autofac nuget package as below.

|EgonsoftHU.Extensions.Logging.Serilog.Autofac|Autofac|
|:-:|:-:|
|4.0.0|4.9.4|
|5.0.0|5.2.0|
|6.0.0|6.3.0|

You can find the release notes [here](https://github.com/gcsizmadia/EgonsoftHU.Extensions.Logging.Serilog.Autofac/releases).

## Summary

These packages use Autofac features so that you can use the `ILogger` interface type for injection instead of manually initializing the private field by calling `Log.Logger.ForContext<TCategoryName>()`.

## Instructions

***First***, determine which version of the Autofac nuget package you use. If you do not use it yet then I suggest using the latest (as of writing 6.3.0) version.

***Next***, install the latest version of the matching major version of the *EgonsoftHU.Extensions.Logging.Serilog.Autofac* [NuGet package](https://www.nuget.org/packages/EgonsoftHU.Extensions.Logging.Serilog.Autofac).

.NET CLI:
```
dotnet add package EgonsoftHU.Extensions.Logging.Serilog.Autofac --version 6.0.0
```

Package Manager:
```pwsh
Install-Package EgonsoftHU.Extensions.Logging.Serilog.Autofac -Version 6.0.0
```

***Next***, add `ConfigureContainer<ContainerBuilder>()` to the Generic Host in `CreateHostBuilder()`.
```C#
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace YourCompany.YourProduct.WebApi
{
    public class Program
    {
        // rest omitted for clarity

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(
                        webHostBuilder =>
                        {
                            webHostBuilder.UseStartup<Startup>();
                        }
                    )
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureContainer<ContainerBuilder>( // <-- Add this method call
                        builder =>
                        {
                            // here comes the magic
                        }
                    );
        }
    }
}
```

***Alternatively***, you can add `ConfigureContainer(ContainerBuilder builder)` to your `Startup.cs` file.

```C#
using Autofac;

using EgonsoftHU.Extensions.DependencyInjection.Autofac;

namespace YourCompany.YourProduct.WebApi
{
    public class Startup
    {
        // rest omitted for clarity.

        public void ConfigureContainer(ContainerBuilder builder) // <-- Add this method
        {
            // here comes the magic
        }
    }
}
```

***Finally***, replace the `// here comes the magic` comment with one of the usage options.

### Usage option #1 - Register the module explicitly

```C#
builder.RegisterModule<EgonsoftHU.Extensions.Logging.Serilog.Autofac.DependencyModule>();
```

### Usage option #2 - Register the module implicitly by using `EgonsoftHU.Extensions.DependencyInjection.Autofac` nuget package

```C#
// Step #1: Configure assembly registry
//
// Add nameof(EgonsoftHU) as an additional assembly file name prefix.
builder.UseDefaultAssemblyRegistry(nameof(YourCompany), nameof(EgonsoftHU));

// Step #2: Register the module that will discover and register all other modules.
//
// This will register EgonsoftHU.Extensions.Logging.Serilog.Autofac.DependencyModule as well.
builder.RegisterModule<EgonsoftHU.Extensions.DependencyInjection.Autofac.DependencyModule>();
```

Click [here](https://github.com/gcsizmadia/EgonsoftHU.Extensions.DependencyInjection.Autofac) to learn more about the `EgonsoftHU.Extensions.DependencyInjection.Autofac` nuget package.
