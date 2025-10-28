using System.Collections.Generic;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models;
using Presentation.Gameplay.Views;
using UnityEngine;

namespace Infrastructure
{
    public class GridCellFactory
    {
        private readonly GameObject _gridCellPrefab;
        private new readonly Dictionary<GridPosition, GameObject> _cells = new();

        public GridCellFactory(GameObject gridCellPrefab)
        {
            _gridCellPrefab = gridCellPrefab;
        }

        public IReadOnlyDictionary<GridPosition, GameObject> Create(CreateGridRequestDTO requestDto, GridView gridView)
        {
            _cells.Clear();

            var startPosition = gridView.GridStartPoint.localPosition;
            var cellPosition = startPosition;
            var cellRotation = Quaternion.identity;
            var cellParent = gridView.transform;
            var sizeX = requestDto.SizeX;
            var sizeY = requestDto.SizeY;
            var disanceBetweenCells = requestDto.DistanceBetweenCells;

            for(var y = 0; y < sizeY; y++) {
                for(var x = 0; x < sizeX; x++) {
                    var cellInstance = Object.Instantiate(_gridCellPrefab, cellPosition, cellRotation, cellParent);
                    _cells.Add(new GridPosition(x, y), cellInstance);

                    cellPosition.x += disanceBetweenCells;
                }

                cellPosition.z += disanceBetweenCells;
                cellPosition.x = startPosition.x;
            }

            return _cells;
        }
    }
}