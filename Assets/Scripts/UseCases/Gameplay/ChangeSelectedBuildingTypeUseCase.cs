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
    public class ChangeSelectedBuildingTypeUseCase : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        [Inject] private BuildingProcessModel _buildingProcessModel;
        [Inject] private ISubscriber<SelectBuilding1Pressed> _selectBuilding1Pressed;
        [Inject] private ISubscriber<SelectBuilding2Pressed> _selectBuilding2Pressed;
        [Inject] private ISubscriber<SelectBuilding3Pressed> _selectBuilding3Pressed;
        [Inject] private IPublisher<SelectedBuildingChanged> _selectedBuildingChanged;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _buildingProcessModel.SelecteBuildingType = BuildingType.House;

            _selectBuilding1Pressed.Subscribe(x => ChangeSelectedBuildingTypeTo(BuildingType.House)).AddTo(_disposable);
            _selectBuilding2Pressed.Subscribe(x => ChangeSelectedBuildingTypeTo(BuildingType.Farm)).AddTo(_disposable);
            _selectBuilding3Pressed.Subscribe(x => ChangeSelectedBuildingTypeTo(BuildingType.Mine)).AddTo(_disposable);
        }

        private void ChangeSelectedBuildingTypeTo(BuildingType type)
        {
            _buildingProcessModel.SelecteBuildingType = type;
            _selectedBuildingChanged.Publish(new SelectedBuildingChanged {NewSelectedBuildingType = type,});
        }
    }
}