using System.Collections.Generic;
using UnityEngine;

namespace Domain.Gameplay.Models
{
    public class BuildingsModel
    {
        public List<Building> PlacedBuildings = new();
        public GameObject SelectedBuilding;
    }
}