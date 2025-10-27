using System;
using Domain.Gameplay.MessagesDTO;
using Infrastructure;
using MessagePipe;
using R3;
using Repositories.Gameplay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public class GridPresenter : IInitializable, IDisposable, ITickable
    {
        private readonly CompositeDisposable _disposable = new();
        [Inject] private IPublisher<CreateGridRequestDTO> _createGridRequestPublisher;
        [Inject] private ISubscriber<CreateLevelRequest> _createLevelRequestSubscriber;
        [Inject] private GridRepository _gridRepository;
        [Inject] private IInputService _inputService;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize() =>
            _createLevelRequestSubscriber.Subscribe(x => Handle(x)).AddTo(_disposable);

        private void Handle(CreateLevelRequest message)
        {
            Debug.Log("Handle");
            _createGridRequestPublisher.Publish(new CreateGridRequestDTO {
                SizeX = _gridRepository.GridSizeX,
                SizeY = _gridRepository.GridSizeY,
                DistanceBetweenCells = _gridRepository.DistanceBetweenCells,
            });
        }

        public void Tick()
        {
            // Debug.Log(_inputService.MousePosition);
        }
    }
}