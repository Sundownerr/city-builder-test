using System.Collections.Generic;
using UnityEngine;

namespace Domain.Gameplay.Models
{
    public class GridModel
    {
        public int SizeX;
        public int SizeY;
        public int DistanceBetweenCells;
        public Dictionary<GameObject, GridPosition> CellOnPosition;
        public List<GridPosition> OccupiedCells;
        public GameObject LastSelectedCell;
        public bool LastSelectedCellFree;
    }
}