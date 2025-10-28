using System;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models;
using MessagePipe;
using R3;
using VContainer;
using VContainer.Unity;

namespace UseCases.Gameplay
{
    public class ChangeSelectedBuildingTypeUseCase : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        [Inject] private IPublisher<BuildingDeselected> _buildingDeselected;
        [Inject] private ISubscriber<BuildingPlaced> _buildingPlaced;
        [Inject] private BuildingProcessModel _buildingProcessModel;
        [Inject] private ISubscriber<SelectHousePressed> _selectBuilding1Pressed;
        [Inject] private ISubscriber<SelectFarmPressed> _selectBuilding2Pressed;
        [Inject] private ISubscriber<SelectMinePressed> _selectBuilding3Pressed;
        [Inject] private IPublisher<SelectedBuildingTypeChanged> _selectedBuildingTypeChanged;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _selectBuilding1Pressed.Subscribe(x => ChangeSelectedBuildingTypeTo(BuildingType.House)).AddTo(_disposable);
            _selectBuilding2Pressed.Subscribe(x => ChangeSelectedBuildingTypeTo(BuildingType.Farm)).AddTo(_disposable);
            _selectBuilding3Pressed.Subscribe(x => ChangeSelectedBuildingTypeTo(BuildingType.Mine)).AddTo(_disposable);
            _buildingPlaced.Subscribe(x => DeselectBuilding()).AddTo(_disposable);
        }

        private void ChangeSelectedBuildingTypeTo(BuildingType type)
        {
            var sameBuilding = type == _buildingProcessModel.SelecteBuildingType &&
                               _buildingProcessModel.BuilingSelected;

            if (sameBuilding) {
                DeselectBuilding();
                return;
            }

            _buildingProcessModel.SelecteBuildingType = type;
            _buildingProcessModel.BuilingSelected = true;
            _selectedBuildingTypeChanged.Publish(new SelectedBuildingTypeChanged {NewSelectedBuildingType = type,});
        }

        private void DeselectBuilding()
        {
            _buildingDeselected.Publish(new BuildingDeselected());
            _buildingProcessModel.BuilingSelected = false;
        }
    }
}