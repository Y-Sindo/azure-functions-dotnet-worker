﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Grpc.Core;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Provides extension methods to work with a <see cref="IHostBuilder"/>.
    /// </summary>
    public static class WorkerHostBuilderExtensions
    {
        /// <summary>
        /// Configures the default set of Functions Worker services to the provided <see cref="IHostBuilder"/>.
        /// The following defaults are configured:
        /// <list type="bullet">
        ///     <item><description>A default set of converters.</description></item>
        ///     <item><description>Integration with Azure Functions logging.</description></item>
        ///     <item><description>Output binding middleware and features.</description></item>
        ///     <item><description>Function execution middleware.</description></item>
        ///     <item><description>Default gRPC support.</description></item>
        /// </list>
        /// </summary>
        /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder ConfigureFunctionsWorkerDefaults(this IHostBuilder builder)
        {
            return builder.ConfigureFunctionsWorkerDefaults(o => { });
        }

        /// <summary>
        /// Configures the default set of Functions Worker services to the provided <see cref="IHostBuilder"/>,
        /// with a delegate to configure a provided <see cref="IFunctionsWorkerApplicationBuilder"/>.
        /// The following defaults are configured:
        /// <list type="bullet">
        ///     <item><description>A default set of converters.</description></item>
        ///     <item><description>Integration with Azure Functions logging.</description></item>
        ///     <item><description>Output binding middleware and features.</description></item>
        ///     <item><description>Function execution middleware.</description></item>
        ///     <item><description>Default gRPC support.</description></item>
        /// </list>
        /// </summary>
        /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="configure">A delegate that will be invoked to configure the provided <see cref="IFunctionsWorkerApplicationBuilder"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder ConfigureFunctionsWorkerDefaults(this IHostBuilder builder, Action<IFunctionsWorkerApplicationBuilder> configure)
        {
            return builder.ConfigureFunctionsWorkerDefaults(configure, o => { });
        }

        /// <summary>
        /// Configures the default set of Functions Worker services to the provided <see cref="IHostBuilder"/>,
        /// with a delegate to configure a provided <see cref="IFunctionsWorkerApplicationBuilder"/> and a delegate to configure the <see cref="WorkerOptions"/>.
        /// The following defaults are configured:
        /// <list type="bullet">
        ///     <item><description>A default set of converters.</description></item>
        ///     <item><description>Integration with Azure Functions logging.</description></item>
        ///     <item><description>Output binding middleware and features.</description></item>
        ///     <item><description>Function execution middleware.</description></item>
        ///     <item><description>Default gRPC support.</description></item>
        /// </list>
        /// </summary>
        /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="configure">A delegate that will be invoked to configure the provided <see cref="IFunctionsWorkerApplicationBuilder"/>.</param>
        /// <param name="configureOptions">A delegate that will be invoked to configure the provided <see cref="WorkerOptions"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder ConfigureFunctionsWorkerDefaults(this IHostBuilder builder, Action<IFunctionsWorkerApplicationBuilder> configure, Action<WorkerOptions> configureOptions)
        {
            return builder.ConfigureFunctionsWorkerDefaults((context, b) => configure(b), configureOptions);
        }

        /// <summary>
        /// Configures the default set of Functions Worker services to the provided <see cref="IHostBuilder"/>,
        /// with a delegate to configure a provided <see cref="HostBuilderContext"/> and an <see cref="IFunctionsWorkerApplicationBuilder"/>.
        /// <list type="bullet">
        ///     <item><description>A default set of converters.</description></item>
        ///     <item><description>Integration with Azure Functions logging.</description></item>
        ///     <item><description>Output binding middleware and features.</description></item>
        ///     <item><description>Function execution middleware.</description></item>
        ///     <item><description>Default gRPC support.</description></item>
        /// </list>
        /// </summary>
        /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="configure">A delegate that will be invoked to configure the provided <see cref="HostBuilderContext"/> and an <see cref="IFunctionsWorkerApplicationBuilder"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder ConfigureFunctionsWorkerDefaults(this IHostBuilder builder, Action<HostBuilderContext, IFunctionsWorkerApplicationBuilder> configure)
        {
            return builder.ConfigureFunctionsWorkerDefaults(configure, o => { });
        }

        /// <summary>
        /// Configures the default set of Functions Worker services to the provided <see cref="IHostBuilder"/>,
        /// with a delegate to configure a provided <see cref="HostBuilderContext"/> and an <see cref="IFunctionsWorkerApplicationBuilder"/>,
        /// and a delegate to configure the <see cref="WorkerOptions"/>.
        /// <list type="bullet">
        ///     <item><description>A default set of converters.</description></item>
        ///     <item><description>Integration with Azure Functions logging.</description></item>
        ///     <item><description>Output binding middleware and features.</description></item>
        ///     <item><description>Function execution middleware.</description></item>
        ///     <item><description>Default gRPC support.</description></item>
        /// </list>
        /// </summary>
        /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="configure">A delegate that will be invoked to configure the provided <see cref="HostBuilderContext"/> and an <see cref="IFunctionsWorkerApplicationBuilder"/>.</param>
        /// <param name="configureOptions">A delegate that will be invoked to configure the provided <see cref="WorkerOptions"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder ConfigureFunctionsWorkerDefaults(this IHostBuilder builder, Action<HostBuilderContext, IFunctionsWorkerApplicationBuilder> configure, Action<WorkerOptions> configureOptions)
        {
            builder.ConfigureServices((context, services) =>
            {
                IFunctionsWorkerApplicationBuilder appBuilder = services.AddFunctionsWorkerDefaults(configureOptions);

                // Call the provided configuration prior to adding default middleware
                configure(context, appBuilder);

                // Add default middleware
                appBuilder.UseDefaultWorkerMiddleware();
            });

            return builder;
        }

        /// <summary>
        /// Configures the core set of Functions Worker services to the provided <see cref="IHostBuilder"/>,
        /// with a delegate to configure a provided <see cref="HostBuilderContext"/> and an <see cref="IFunctionsWorkerApplicationBuilder"/>,
        /// and a delegate to configure the <see cref="WorkerOptions"/>.
        /// NOTE: You must configure required services for an operational worker when using this method.
        /// For a method that builds a Worker with a default set of services, use <see cref="ConfigureFunctionsWorkerDefaults(IHostBuilder)"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="configure">A delegate that will be invoked to configure the provided <see cref="HostBuilderContext"/> and an <see cref="IFunctionsWorkerApplicationBuilder"/>.</param>
        /// <param name="configureOptions">A delegate that will be invoked to configure the provided <see cref="WorkerOptions"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder ConfigureFunctionsWorker(this IHostBuilder builder, Action<HostBuilderContext, IFunctionsWorkerApplicationBuilder> configure, Action<WorkerOptions> configureOptions)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.ConfigureServices((context, services) =>
            {
                IFunctionsWorkerApplicationBuilder appBuilder = services.AddFunctionsWorkerCore(configureOptions);

                configure(context, appBuilder);
            });

            return builder;
        }
    }
}