using Bussines.Interfaces;
using Core.Manager.ICore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Bussines.Implementacion;
using Core.Manager.Core;
using Repository.Base;

namespace Core
{
    public class Register
    {
        public Register()
        {
        }

        public static void RegisterDI<T>(IServiceCollection services, IConfiguration configuration)
        where T : DbContext
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IConsumeAPIService, ConsumeAPIService>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IJourneyService, JourneyService>();
            services.AddScoped<ITransportService, TransportService>();
            services.AddScoped<IRoutesCalculate, RoutesCalculate>();

            services.AddScoped<IFlightManager, FlightManager>();
            services.AddScoped<IJourneyManager, JourneyManager>();
            services.AddScoped<ITransportManager, TransportManager>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, T>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}