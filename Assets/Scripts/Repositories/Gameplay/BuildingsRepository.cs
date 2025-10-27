using System.Collections.Generic;
using Domain.Gameplay.Models;
using UnityEngine;

namespace Repositories.Gameplay
{
    [CreateAssetMenu(menuName = "BuildingsRepository",
        fileName = "Gameplay/BuildingsRepository")]
    public class BuildingsRepository : ScriptableObject
    {
        public List<BuildingConfig> BuildingConfigs;

        public BuildingConfig GetBuildingConfigOfType(BuildingType type)
        {
            foreach (var buildingConfig in BuildingConfigs) {
                if (buildingConfig.Type == type)
                    return buildingConfig;
            }
            
            Debug.LogError($"Missing building of type {type}");
            return null;
        }
    }
}