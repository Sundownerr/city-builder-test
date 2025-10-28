using System;
using Domain.Application.MessagesDTO;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models;
using MessagePipe;
using R3;
using VContainer;
using VContainer.Unity;

namespace UseCases.Gameplay
{
    public class CheckCanPlaceBuildingUseCase : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        [Inject] private BuildingProcessModel _buildingProcessModel;
        [Inject] private GridModel _gridModel;
        [Inject] private IPublisher<PlaceBuildingAllowed> _placeBuildingAllowed;
        [Inject] private IPublisher<PlaceBuildingNotAllowed> _placeBuildingNotAllowed;
        [Inject] private IPublisher<PlaceBuildingRequest> _placeBuildingRequest;
        [Inject] private ISubscriber<SelectedCellChanged> _selectedCellChanged;
        [Inject] private ISubscriber<SelectPressed> _selectPressed;
        [Inject] private ISubscriber<BuildingPlaced> _buildingPlaced;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _selectPressed.Subscribe(x => CheckCanPlaceBuilding()).AddTo(_disposable);
            _selectedCellChanged.Subscribe(x => UpdatePlacingBuildingAllowed()).AddTo(_disposable);
            _buildingPlaced.Subscribe(x => UpdatePlacingBuildingAllowed()).AddTo(_disposable);
        }

        private bool UpdatePlacingBuildingAllowed()
        {
            var allowed = _gridModel.LastSelectedCell != null &&
                          _gridModel.LastSelectedCellFree;

            _buildingProcessModel.PlacingBuildingAllowed = allowed;

            if (allowed)
                _placeBuildingAllowed.Publish(new PlaceBuildingAllowed());
            else
                _placeBuildingNotAllowed.Publish(new PlaceBuildingNotAllowed());

            return allowed;
        }

        private void CheckCanPlaceBuilding()
        {
            var placingBuildingAllowed = UpdatePlacingBuildingAllowed();

            if (placingBuildingAllowed)
                _placeBuildingRequest.Publish(new PlaceBuildingRequest());
        }
    }
}