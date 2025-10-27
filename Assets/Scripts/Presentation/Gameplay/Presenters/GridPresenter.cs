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
    public class GridPresenter : IInitializable, IDisposable, ITickable
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly RaycastHit[] _raycastHits = new RaycastHit[1];
        [Inject] private Camera _camera;
        [Inject] private IPublisher<CreateGridRequestDTO> _createGridRequestPublisher;
        [Inject] private ISubscriber<CreateLevelRequest> _createLevelRequestSubscriber;
        [Inject] private GridRepository _gridRepository;
        [Inject] private GridView _gridView;
        private int _layerMask;
        [Inject] private IRaycastFromCameraService _raycastFromCameraService;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _createLevelRequestSubscriber.Subscribe(x => SendCreateGridRequest(x)).AddTo(_disposable);
            _layerMask = LayerMask.GetMask("Selectable");
        }

        public void Tick() =>
            HighlightCell();

        private void HighlightCell()
        {
            _gridView.GridCellHighlight.gameObject.SetActive(false);

            var ray = _raycastFromCameraService.Ray;

            if (Physics.RaycastNonAlloc(ray.origin, ray.direction, _raycastHits, Mathf.Infinity, _layerMask) > 0) {
                _gridView.GridCellHighlight.gameObject.SetActive(true);
                _gridView.GridCellHighlight.transform.position = _raycastHits[0].transform.position;
            }
        }

        private void SendCreateGridRequest(CreateLevelRequest message) =>
            _createGridRequestPublisher.Publish(new CreateGridRequestDTO {
                SizeX = _gridRepository.GridSizeX,
                SizeY = _gridRepository.GridSizeY,
                DistanceBetweenCells = _gridRepository.DistanceBetweenCells,
            });
    }
}