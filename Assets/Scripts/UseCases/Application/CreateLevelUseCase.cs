using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UseCases.Application
{
    public class CreateLevelUseCase : IStartable
    {
        [Inject] private IPublisher<CreateLevelRequest> _createLevelRequestPublisher;

        public void Start()
        {
            _createLevelRequestPublisher.Publish(new CreateLevelRequest());
        }
    }
}