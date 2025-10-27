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
        [Inject] private Camera _camera;
        [Inject] private IPublisher<CreateGridRequestDTO> _createGridRequestPublisher;
        [Inject] private ISubscriber<CreateLevelRequest> _createLevelRequestSubscriber;
        [Inject] private GridRepository _gridRepository;
        [Inject] private GridView _gridView;
        [Inject] private IInputService _inputService;
        private int _layerMask;
        private readonly RaycastHit[] _raycastHits = new RaycastHit[1];

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _createLevelRequestSubscriber.Subscribe(x => SendCreateGridRequest(x)).AddTo(_disposable);
            _layerMask = LayerMask.GetMask("Selectable");
        }

        public void Tick()
        {
            var ray = _camera.ScreenPointToRay(_inputService.MousePosition);
            var origin = _camera.transform.position;

            HighlightCell(origin, ray);
        }

        private void HighlightCell(Vector3 origin, Ray ray)
        {
            _gridView.GridCellHighlight.gameObject.SetActive(false);

            if (Physics.RaycastNonAlloc(origin, ray.direction, _raycastHits, Mathf.Infinity, _layerMask) > 0) {
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