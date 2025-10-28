using System;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models;
using MessagePipe;
using R3;
using VContainer;
using VContainer.Unity;

namespace UseCases.Gameplay
{
    public class ChangeSelectedCellUseCase : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        [Inject] private ISubscriber<CellDeselected> _cellDeselected;
        [Inject] private GridModel _gridModel;
        [Inject] private ISubscriber<SelectedCellChanged> _selectedCellChanged;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _selectedCellChanged.Subscribe(x => ChangeSelectedCell(x)).AddTo(_disposable);
            _cellDeselected.Subscribe(x => DeselectCell()).AddTo(_disposable);
        }

        private void ChangeSelectedCell(SelectedCellChanged selectedCellChanged)
        {
            var cellGameObject = selectedCellChanged.NewSelectedCell;
            var cellPosition = _gridModel.CellToGridPosition[cellGameObject];

            _gridModel.LastSelectedCell = cellGameObject;
            _gridModel.LastSelectedCellPosition = cellPosition;
            _gridModel.LastSelectedCellFree = !_gridModel.OccupiedCells.Contains(cellPosition);
        }

        private void DeselectCell() =>
            _gridModel.LastSelectedCell = null;
    }
}