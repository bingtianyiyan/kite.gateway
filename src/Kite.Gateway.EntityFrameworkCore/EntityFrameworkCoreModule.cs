﻿using Microsoft.Extensions.DependencyInjection;
using Kite.Gateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;
using Volo.Abp;
using Microsoft.EntityFrameworkCore;

namespace Kite.Gateway.EntityFrameworkCore
{
    [DependsOn(
        typeof(DomainModule),
        typeof(AbpEntityFrameworkCoreMySQLModule)
        )]
    public class EntityFrameworkCoreModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });
            context.Services.AddAbpDbContext<KiteDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var dbContext = context.ServiceProvider.GetService<KiteDbContext>();
            if (dbContext != null)
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
