using System;
using Domain.Gameplay.MessagesDTO;
using Infrastructure;
using MessagePipe;
using Presentation.Gameplay.Views;
using R3;
using Repositories.Gameplay;
using VContainer;
using VContainer.Unity;
using Grid = Domain.Gameplay.Models.Grid;

namespace UseCases.Gameplay
{
    public class CreateGridUseCase : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        [Inject] private ISubscriber<CreateGridRequestDTO> _createGridRequestDTO;
        private GridCellFactory _gridCellFactory;
        [Inject] private GridRepository _gridRepository;
        [Inject] private GridView _gridView;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _gridCellFactory = new GridCellFactory(_gridRepository.CellPrefab);
            _createGridRequestDTO.Subscribe(x => Handle(x)).AddTo(_disposable);
        }

        public void Handle(CreateGridRequestDTO message)
        {
            var grid = new Grid {
                SizeX = message.SizeX,
                SizeY = message.SizeY,
                DistanceBetweenCells = message.DistanceBetweenCells,
            };

            _gridCellFactory.Create(grid, _gridView);
        }
    }
}