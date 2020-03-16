using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.MyProjectName;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName.Host.HttpApi
{
    [DependsOn(
        typeof(MyProjectNameApplicationModule),
        typeof(MyProjectNameHttpApiModule),
        typeof(EntityFrameworkCore.MyProjectNameEntityFrameworkCoreModule)
        )]
    public class MyProjectNameHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddJsonSubtypesConverterProfile<MyProjectNameApplicationModule>();
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(MyProjectNameApplicationModule).Assembly, action => action.RootPath = "MyProjectName");
            });
        }
    }
}
