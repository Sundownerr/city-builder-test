using System;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models;
using Infrastructure;
using MessagePipe;
using R3;
using Repositories.Gameplay;
using VContainer;
using VContainer.Unity;

namespace UseCases.Gameplay
{
    public class PlaceBuildingUseCase : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        private BuildingFactory _buildingFactory;
        [Inject] private BuildingProcessModel _buildingProcessModel;
        [Inject] private BuildingsModel _buildingsModel;
        [Inject] private BuildingsRepository _buildingsRepository;
        [Inject] private GridModel _gridModel;
        [Inject] private ISubscriber<PlaceBuildingRequest> _placeBuildingRequest;
        [Inject] private IPublisher<BuildingPlaced> _buildingPlaced;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _buildingFactory = new BuildingFactory();
            _placeBuildingRequest.Subscribe(x => PlaceBuilding()).AddTo(_disposable);
        }

        private void PlaceBuilding()
        {
            if (!_buildingProcessModel.PlacingBuildingAllowed)
                return;

            var buildingType = _buildingProcessModel.SelecteBuildingType;
            var buildingConfig = _buildingsRepository.GetBuildingConfigOfType(buildingType);
           
            _buildingFactory.CreateBuilding(
                buildingConfig,
                _gridModel.LastSelectedCellPosition,
                _gridModel.LastSelectedCell.transform.position);

            _gridModel.OccupiedCells.Add(_gridModel.LastSelectedCellPosition);
            _gridModel.LastSelectedCellFree = false;

            var building = new Building() {
                Type = buildingType,
                Income = buildingConfig.Income,
                Level = 0
            };
            
            _buildingsModel.PlacedBuildings.Add(building);

            _buildingPlaced.Publish(new BuildingPlaced());
        }
    }
}