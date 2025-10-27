using Presentation.Gameplay.Presenters;
using Presentation.Gameplay.Views;
using Repositories.Gameplay;
using UseCases.Gameplay;
using VContainer;
using VContainer.Unity;

namespace Installers.Application
{
    public class GameplayInstaller : LifetimeScope
    {
        public GridRepository GridRepository;
        public GridView GridView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(GridRepository);
            builder.RegisterInstance(GridView);
            builder.Register<CreateGridUseCase>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GridPresenter>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}