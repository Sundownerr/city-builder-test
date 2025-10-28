using System;
using Domain.Application.MessagesDTO;
using Infrastructure;
using MessagePipe;
using R3;
using VContainer.Unity;

namespace UseCases.Gameplay
{
    public class PlaceBuildingUseCase : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        private BuildingFactory _buildingFactory;
        private ISubscriber<SelectPressed> _selectPressed;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _buildingFactory = new BuildingFactory();
            _selectPressed.Subscribe(x => PlaceBuilding()).AddTo(_disposable);
        }

        private void PlaceBuilding() { }
    }
}