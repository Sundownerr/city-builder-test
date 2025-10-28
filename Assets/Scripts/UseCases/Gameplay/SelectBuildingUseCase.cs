using System;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models;
using Infrastructure;
using MessagePipe;
using R3;
using VContainer;
using VContainer.Unity;

namespace UseCases.Gameplay
{
    public class SelectBuildingUseCase : IInitializable, IDisposable
    {
        [Inject] private BuildingsModel _buildingsModel;
        private readonly CompositeDisposable _disposable = new();
        [Inject] private ObjectSelectionService _objectSelectionService;
        [Inject] private IPublisher<SelectedBuildingChanged> _selectedBuildingChanged;
        [Inject] private IPublisher<BuildingDeselected> _buildingDeselected;
        [Inject] private ISubscriber<SelectPressed> _selectPressed;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize() =>
            _selectPressed.Subscribe(x => SelectBuilding()).AddTo(_disposable);

        private void SelectBuilding()
        {
            if (!_objectSelectionService.BuildingSelected) {
                _buildingDeselected.Publish(new BuildingDeselected());
                _buildingsModel.SelectedBuilding = null;
                return;
            }

            _buildingsModel.SelectedBuilding = _objectSelectionService.LastSelectedBuilding;

            _selectedBuildingChanged.Publish(new SelectedBuildingChanged {
                NewSelectedBuilding = _objectSelectionService.LastSelectedBuilding,
            });
        }
    }
}