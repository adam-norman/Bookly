using Application.Abstraction.Behaviors;
using Domain.Entities.Bookings.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DepedencyInjection).Assembly);

                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssemblies(new[] { typeof(DepedencyInjection).Assembly });
            services.AddTransient<PricingService>();
            return services;
        }
    }
}
