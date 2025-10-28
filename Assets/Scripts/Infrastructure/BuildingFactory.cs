using Domain.Gameplay.Models;
using Repositories.Gameplay;
using UnityEngine;

namespace Infrastructure
{
    public class BuildingFactory
    {
        public void CreateBuilding(BuildingConfig config, GridPosition gridPosition, Vector3 gridPositionVector3)
        {
            var buildingInstance = Object.Instantiate(config.Prefab, gridPositionVector3, Quaternion.identity);
        }
    }
}