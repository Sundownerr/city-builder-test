using System;
using System.Collections.Generic;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models;
using Infrastructure;
using MessagePipe;
using Presentation.Gameplay.Views;
using R3;
using Repositories.Gameplay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UseCases.Gameplay
{
    public class CreateGridUseCase : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        [Inject] private ISubscriber<CreateGridRequestDTO> _createGridRequestDTO;
        private GridCellFactory _gridCellFactory;
        [Inject] private GridModel _gridModel;
        [Inject] private GridRepository _gridRepository;
        [Inject] private GridView _gridView;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _gridCellFactory = new GridCellFactory(_gridRepository.CellPrefab);
            _createGridRequestDTO.Subscribe(x => CreateGrid(x)).AddTo(_disposable);
        }

        public void CreateGrid(CreateGridRequestDTO request)
        {
            IReadOnlyDictionary<GridPosition, GameObject> cells = _gridCellFactory.Create(request, _gridView);

            _gridModel.SizeX = request.SizeX;
            _gridModel.SizeY = request.SizeY;
            _gridModel.DistanceBetweenCells = request.DistanceBetweenCells;
            _gridModel.CellToGridPosition = new Dictionary<GameObject, GridPosition>();
            _gridModel.GridToCellPosition = new Dictionary<GridPosition, GameObject>();
            _gridModel.OccupiedCells = new List<GridPosition>();

            foreach (var (gridPosition, cell) in cells) {
                _gridModel.CellToGridPosition.Add(cell, gridPosition);
                _gridModel.GridToCellPosition.Add(gridPosition, cell);
            }
        }
    }
}