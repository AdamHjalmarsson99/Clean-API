﻿using Infrastructure.Database;
using Infrastructure.MySQLDb;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<MockDatabase>();
            services.AddDbContext<RealDatabase>();
            return services;

            
        }
    }
}
