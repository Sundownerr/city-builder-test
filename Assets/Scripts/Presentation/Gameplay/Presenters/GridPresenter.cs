using System;
using Domain.Gameplay.MessagesDTO;
using Infrastructure;
using MessagePipe;
using Presentation.Gameplay.Views;
using R3;
using Repositories.Gameplay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public class GridPresenter : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        [Inject] private ISubscriber<CellDeselected> _cellDeselected;
        [Inject] private IPublisher<CreateGridRequestDTO> _createGridRequestPublisher;
        [Inject] private ISubscriber<CreateLevelRequest> _createLevelRequestSubscriber;
        [Inject] private GridRepository _gridRepository;
        [Inject] private GridView _gridView;
        [Inject] private IRaycastFromCameraService _raycastFromCameraService;
        [Inject] private ISubscriber<SelectedCellChanged> _selectedCellChanged;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _createLevelRequestSubscriber.Subscribe(x => SendCreateGridRequest(x)).AddTo(_disposable);
            _selectedCellChanged.Subscribe(x => HighlightCell(x.NewSelectedCell)).AddTo(_disposable);
            _cellDeselected.Subscribe(x => HideHighlight()).AddTo(_disposable);
        }

        private void HideHighlight() =>
            _gridView.GridCellHighlight.gameObject.SetActive(false);

        private void HighlightCell(GameObject cell)
        {
            var highlight = _gridView.GridCellHighlight;

            highlight.gameObject.SetActive(true);
            highlight.transform.position = cell.transform.position;
        }

        private void SendCreateGridRequest(CreateLevelRequest message) =>
            _createGridRequestPublisher.Publish(new CreateGridRequestDTO {
                SizeX = _gridRepository.GridSizeX,
                SizeY = _gridRepository.GridSizeY,
                DistanceBetweenCells = _gridRepository.DistanceBetweenCells,
            });
    }
}