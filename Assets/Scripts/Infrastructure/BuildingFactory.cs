using Domain.Gameplay.Models;
using Repositories.Gameplay;
using VContainer;

namespace Infrastructure
{
    public class BuildingFactory
    {
        [Inject] private BuildingsRepository _buildingsRepository;
        
        public void CreateBuilding(BuildingType type, GridPosition gridPosition)
        {
            var buildingConfig = _buildingsRepository.GetBuildingConfigOfType(type);
            // var buildingPositionVector3 = 
            // var buildingInstance = UnityEngine.Object.Instantiate(buildingConfig.Prefab);
        }
    }
}