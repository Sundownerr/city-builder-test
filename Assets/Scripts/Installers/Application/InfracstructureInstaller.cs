using Domain.Gameplay.MessagesDTO;
using Infrastructure;
using MessagePipe;
using Presentation.Gameplay.Presenters;
using Presentation.Gameplay.Views;
using Repositories.Gameplay;
using UseCases.Application;
using UseCases.Gameplay;
using VContainer;
using VContainer.Unity;

namespace Installers.Application
{
    public class InfracstructureInstaller : LifetimeScope
    {
        public GridRepository GridRepository;
        public GridView GridView;

        protected override void Configure(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
            builder.RegisterBuildCallback(c => { GlobalMessagePipe.SetProvider(c.AsServiceProvider()); });
            builder.RegisterMessageBroker<CreateLevelRequest>(options);
            builder.RegisterMessageBroker<CreateGridRequestDTO>(options);

            builder.RegisterEntryPoint<CreateLevelUseCase>();
            builder.RegisterInstance(GridRepository);
            builder.RegisterInstance(GridView);
            builder.Register<CreateGridUseCase>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GridPresenter>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.RegisterEntryPoint<InputService>();
        }
    }
}