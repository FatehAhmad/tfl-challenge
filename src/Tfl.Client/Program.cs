using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tfl.Application.CommonInterfaces.DataProviders;
using Tfl.Common.Extensions;
using Tfl.Application.CommonInterfaces.Handlers;
using Tfl.Domain.RoadStatus.Models.RequestModels;
using Tfl.Domain.RoadStatus.Models.ResponseModels;
using Tfl.Client.Configurations;
using Tfl.Client.Controllers;
using Tfl.Client.Validators;
using Tfl.Infrastructure.Implementations.RoadStatus;
using System.Reflection;
using Tfl.Application.CommonInterfaces.RouteBuilders;
using Tfl.Application.CommonInterfaces.Mappers;

namespace Tfl.Client
{
    /// <summary>
    /// Main Program
    /// DI Container
    /// </summary>
    public class Program
    {
        private readonly ILogger<Program> logger;
        private readonly RoadStatusController roadStatusController;
        private readonly IConfigurationBuilder configurationBuilder;

        public Program(ILogger<Program> logger,
            RoadStatusController roadStatusController,
            IConfigurationBuilder configurationBuilder)
        {
            this.logger = logger;
            this.roadStatusController = roadStatusController;
            this.configurationBuilder = configurationBuilder;
        }

        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine($"Road Id is invalid");
                Environment.Exit(1);
            }

            var host = CreateHostBuilder(args).Build();
            var result = await host.Services.GetRequiredService<Program>().Run(args[0]);

            foreach (var roadStatus in result)
            {
                Console.WriteLine($"The status of the {roadStatus.Id} is as follows");
                Console.WriteLine($"        Road Status is {roadStatus.StatusSeverity}");
                Console.WriteLine($"        Road Status Description is {roadStatus.statusSeverityDescription}");
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var assembly = typeof(RoadStatusHandler).Assembly;

                    RegisterCommon(services);
                    RegisterServices(assembly, typeof(IHandler<,>), services);
                    RegisterServices(assembly, typeof(IDataProvider), services);
                    RegisterServices(assembly, typeof(IRouteBuilder), services);
                    RegisterServices(assembly, typeof(IMapper<,>), services);
                    RegisterHttpClients(services, hostContext.Configuration);
                });

        private static void RegisterCommon(IServiceCollection services)
        {
            services.AddTransient<Program>();
            services.AddTransient<RoadStatusRequestValidator>();
            services.AddTransient<RoadStatusController>();
            services.AddTransient<IConfigurationBuilder, ConfigurationBuilder>();
        }

        private static void RegisterServices(Assembly assembly, Type serviceType, IServiceCollection services, bool allowDerivedInterfaces = false)
        {
            var implementations = assembly.GetTypes().Where(type => type.ImplementsInterface(serviceType) && !type.IsAbstract).ToArray();
            foreach (var implementation in implementations)
            {
                IEnumerable<Type> fullInterfacesList = implementation.GetInterfaces();
                IEnumerable<Type> derivedInterfaces = implementation.BaseType?.GetInterfaces().EmptyIfNull()!;
                IEnumerable<Type> notDerivedInterfaces = allowDerivedInterfaces ? fullInterfacesList : fullInterfacesList.Where(item => !derivedInterfaces.Contains(item)).ToArray();
                IEnumerable<Type> detailedInterfaces = notDerivedInterfaces.Where(item => item.GetInterfaces().Contains(serviceType));
                IEnumerable<Type> suitableInterfaces = detailedInterfaces.Any() ? detailedInterfaces : notDerivedInterfaces.Where(item => item.Name == serviceType.Name);
                var expandedInterface = suitableInterfaces.Count() == 1 ? suitableInterfaces.First() : null;
                if (expandedInterface == null)
                {
                    continue;
                }
                services.AddTransient(implementation);
                services.AddTransient(expandedInterface, implementation);

                Console.WriteLine($"Registered {expandedInterface.Name} with {implementation.Name}");
            }
        }

        private static void RegisterHttpClients(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("tfl", client =>
            {
                var config = configuration.GetSection("TflSettings");
                client.BaseAddress = new Uri(config.GetValue<string>("ApiBaseUrl"));
            });
        }

        private async Task<IEnumerable<RoadStatusResponse>> Run(string args)
        {
            logger.LogInformation("Tfl client started.");

            return await roadStatusController.Get(BuildRequest(args), CancellationToken.None);
        }

        private RoadStatusRequest BuildRequest(string args)
        {
            var config = configurationBuilder.AddJsonFile("appsettings.json").Build();
            var settings = config.GetSection("TflSettings").Get<TflSettings>();

            return new RoadStatusRequest { Id = args, AppId = settings.AppId, ApiKey = settings.AppKey, Path = settings.Path };
        }
    }
}

