using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UseCases.Application
{
    public class CreateLevelUseCase : IInitializable
    {
        [Inject] private IPublisher<CreateLevelRequest> _createLevelRequestPublisher;

        public void Initialize()
        {
            _createLevelRequestPublisher.Publish(new CreateLevelRequest());
        }
    }
}