using UseCases.Application;
using VContainer;
using VContainer.Unity;

namespace Installers.Application
{
    public class ApplicationInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<CreateLevelUseCase>();
    }
}