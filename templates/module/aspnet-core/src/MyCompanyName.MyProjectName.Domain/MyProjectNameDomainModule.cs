using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameDomainSharedModule),
        typeof(FS.Abp.Domain.AbpDddDomainModule)
        )]
    public class MyProjectNameDomainModule : AbpModule
    {

    }
}
