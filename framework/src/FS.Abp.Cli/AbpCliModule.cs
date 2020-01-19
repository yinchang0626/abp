using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Octokit;
using Volo.Abp.Autofac;
using Volo.Abp.Cli;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace FS.Abp.Cli
{
    [DependsOn(
        typeof(AbpCliCoreModule),
        typeof(AbpAutofacModule),
        typeof(AbpVirtualFileSystemModule)
    )]
    public class AbpCliModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IGitHubClient>(x=> 
            {
                var opt = x.GetService<IOptions<OctokitOptions>>();
                return new GitHubClient(new Octokit.ProductHeaderValue(opt.Value.ProductHeaderName));
            });

            Configure<OctokitOptions>(options =>
            {
                options.ProductHeaderName = "YinChang0626";
                options.OwnerName = "YinChang0626";
                options.RepositoryName = "abp";
                options.BranchName = "feature/fs_abp_cli";
            });
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpCliModule>("FS.Abp.Cli");
            });

        }

    }
}