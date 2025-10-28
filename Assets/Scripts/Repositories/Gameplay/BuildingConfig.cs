using Domain.Gameplay.Models;
using UnityEngine;

namespace Repositories.Gameplay
{
    [CreateAssetMenu(menuName = "BuildingConfig",
        fileName = "Gameplay/BuildingConfig")]
    public class BuildingConfig : ScriptableObject
    {
        public string Name;
        public BuildingType Type;
        public int Cost;
        
        public GameObject Prefab;
    }
}