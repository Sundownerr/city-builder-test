using System;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models;
using Infrastructure;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UseCases.Gameplay
{
    public class ChangeSelectedCellUseCase : IInitializable, IDisposable, ITickable
    {
        private readonly RaycastHit[] _raycastHits = new RaycastHit[1];
        [Inject] private IPublisher<CellDeselected> _cellDeselected;
        [Inject] private GridModel _gridModel;
        private int _layerMask;
        [Inject] private IRaycastFromCameraService _raycastFromCameraService;
        [Inject] private IPublisher<SelectedCellChanged> _selectedCellChanged;

        public void Dispose() { }

        public void Initialize() =>
            _layerMask = LayerMask.GetMask("Selectable");

        public void Tick()
        {
            var ray = _raycastFromCameraService.Ray;

            if (Physics.RaycastNonAlloc(ray.origin, ray.direction, _raycastHits, Mathf.Infinity, _layerMask) <= 0) {
                DeselectCell();
                return;
            }

            var cellGameObject = _raycastHits[0].transform.gameObject;
            var cellPosition = _gridModel.CellToGridPosition[cellGameObject];

            _gridModel.LastSelectedCell = cellGameObject;
            _gridModel.LastSelectedCellPosition = cellPosition;
            _gridModel.LastSelectedCellFree = !_gridModel.OccupiedCells.Contains(cellPosition);

            _selectedCellChanged.Publish(new SelectedCellChanged {
                NewSelectedCell = cellGameObject,
            });
        }

        private void DeselectCell()
        {
            _cellDeselected.Publish(new CellDeselected());
            _gridModel.LastSelectedCell = null;
        }
    }
}