using Domain.Gameplay.MessagesDTO;
using Infrastructure;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Installers.Application
{
    public class InfracstructureInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
            builder.RegisterBuildCallback(c => { GlobalMessagePipe.SetProvider(c.AsServiceProvider()); });
            builder.RegisterMessageBroker<CreateLevelRequest>(options);
            builder.RegisterMessageBroker<CreateGridRequestDTO>(options);
            builder.RegisterMessageBroker<SelectedCellChanged>(options);
            builder.RegisterMessageBroker<CellDeselected>(options);

            builder.RegisterEntryPoint<InputService>();
        }
    }
}