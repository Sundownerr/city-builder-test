using Presentation.Gameplay.Views;
using UnityEngine;
using Grid = Domain.Gameplay.Models.Grid;

namespace Infrastructure
{
    public class GridCellFactory
    {
        private readonly GameObject _gridCellPrefab;

        public GridCellFactory(GameObject gridCellPrefab)
        {
            _gridCellPrefab = gridCellPrefab;
        }

        public void Create(Grid grid, GridView gridView)
        {
            var startPosition = gridView.GridStartPoint.localPosition;
            var cellPosition = startPosition;
            var cellRotation = Quaternion.identity;
            var cellParent = gridView.transform;
            var sizeX = grid.SizeX;
            var sizeY = grid.SizeY;
            var disanceBetweenCells = grid.DistanceBetweenCells;

            for(var i = 0; i < sizeY; i++) {
                for(var j = 0; j < sizeX; j++) {
                    var cellInstance = Object.Instantiate(_gridCellPrefab, cellPosition, cellRotation, cellParent);
                    cellPosition.x += disanceBetweenCells;
                }

                cellPosition.z += disanceBetweenCells;
                cellPosition.x = startPosition.x;
            }
        }
    }
}