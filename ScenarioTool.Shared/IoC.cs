using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScenarioTool.Shared.Entity;
using ScenarioTool.Shared.Providers;
using ScenarioTool.Shared.Services;
using ScenarioTool.Shared.Services.ActionHandlers;

namespace ScenarioTool.Shared
{
    public static class IoC
    {
        public delegate IStepActionHandler StepActionServiceResolver(ScenarioActionType key);

        public static IServiceCollection AddScenarioCore(this IServiceCollection services, IConfiguration configuration) 
        {
            var settings = configuration.GetSection(nameof(CoreSettings)).Get<CoreSettings>()
                ?? throw new InvalidDataException(nameof(CoreSettings));

            services.AddSingleton(settings);

            services.AddTransient<IScenarioService, ScenarioService>();
            services.AddTransient<IScenarioExecutor, ScenarioExecutor>();
            services.AddTransient<IStepActionProvider, StepActionProvider>();

            //action handlers            
            services.AddTransient<DefaultActionHandler>();
            services.AddTransient<CopyFolderActionHandler>();
            services.AddTransient<RunProcessActionHandler>();
            services.AddTransient<TextReplaceActionHandler>();            

            services.AddStepActionHandlers();

            return services;
        }

        private static IServiceCollection AddStepActionHandlers(this IServiceCollection services)
            => services.AddTransient<StepActionServiceResolver>(serviceProvider => key =>
                key switch
                {
                    ScenarioActionType.CopyFolder => GetActionHandler<CopyFolderActionHandler>(serviceProvider),
                    ScenarioActionType.RunProcess => GetActionHandler<RunProcessActionHandler>(serviceProvider),
                    ScenarioActionType.TextReplace => GetActionHandler<TextReplaceActionHandler>(serviceProvider),

                    _ => throw new KeyNotFoundException(),
                });

        private static IStepActionHandler GetActionHandler<T>(IServiceProvider serviceProvider) where T:IStepActionHandler
            => serviceProvider.GetService<T>() ?? (IStepActionHandler)new DefaultActionHandler();

       
    }
}
