using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure
{
    public class ObjectSelectionService : IInitializable, ITickable
    {
        private readonly RaycastHit[] _raycastHits = new RaycastHit[10];
        private TagHandle _buildingTag;
        [Inject] private IPublisher<CellDeselected> _cellDeselected;
        private TagHandle _cellTag;
        private GameObject _lastSelectedBuilding;
        private GameObject _lastSelectedCell;
        private int _layerMask;
        [Inject] private IRaycastFromCameraService _raycastFromCameraService;
       
        [Inject] private IPublisher<SelectedCellChanged> _selectedCellChanged;

        public bool CellSelected { get; private set; }
        public GameObject LastSelectedCell { get; private set; }
        public GameObject LastSelectedBuilding { get; private set; }
        public bool BuildingSelected { get; private set; }

        public void Initialize()
        {
            _layerMask = LayerMask.GetMask("Selectable");
            _cellTag = TagHandle.GetExistingTag("Cell");
            _buildingTag = TagHandle.GetExistingTag("Building");
        }

        public void Tick()
        {
            var ray = _raycastFromCameraService.Ray;
            var hits = Physics.RaycastNonAlloc(ray.origin, ray.direction, _raycastHits, Mathf.Infinity, _layerMask);

            if (hits <= 0) {
                DeselectCell();
                DeselectBuilding();
                return;
            }

            var newCellSelected = false;
            var newBuildingSelected = false;
            var sameCellSelected = false;
            var sameBuildingSelected = false;

            for(var i = 0; i < hits; i++) {
                var hit = _raycastHits[i];
                var hitGameObject = hit.transform.gameObject;

                if (!newCellSelected && hitGameObject.CompareTag(_cellTag)) {
                    if (_lastSelectedCell == hitGameObject) {
                        sameCellSelected = true;
                    }
                    else {
                        LastSelectedCell = hitGameObject;
                        _lastSelectedCell = hitGameObject;

                        newCellSelected = true;
                        _selectedCellChanged.Publish(new SelectedCellChanged {
                            NewSelectedCell = hitGameObject,
                        });
                    }
                }

                if (!newBuildingSelected && hitGameObject.CompareTag(_buildingTag)) {
                    if (_lastSelectedBuilding == hitGameObject) {
                        sameBuildingSelected = true;
                    }
                    else {
                        LastSelectedBuilding = hitGameObject;
                        _lastSelectedBuilding = hitGameObject;

                        newBuildingSelected = true;
                      
                    }
                }
            }

            if (!sameCellSelected)
                CellSelected = newCellSelected;

            if (!sameBuildingSelected)
                BuildingSelected = newBuildingSelected;
        }

        private void DeselectCell()
        {
            _cellDeselected.Publish(new CellDeselected());
            CellSelected = false;
            _lastSelectedCell = null;
            LastSelectedCell = null;
        }

        private void DeselectBuilding()
        {
            BuildingSelected = false;
            _lastSelectedBuilding = null;
            LastSelectedBuilding = null;
        }
    }
}