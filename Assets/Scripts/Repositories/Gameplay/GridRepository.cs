using UnityEngine;

namespace Repositories.Gameplay
{
    [CreateAssetMenu(menuName = "GridRepository",
        fileName = "Gameplay/GridRepository")]
    public class GridRepository : ScriptableObject
    {
        public int GridSizeX;
        public int GridSizeY;
        public int DistanceBetweenCells;
        public GameObject CellPrefab;
    }
}