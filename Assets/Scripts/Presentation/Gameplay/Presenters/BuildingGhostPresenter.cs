using System;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using Presentation.Gameplay.Views;
using R3;
using Repositories.Gameplay;
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public class BuildingGhostPresenter : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        [Inject] private BuildingsRepository _buildingsRepository;
        [Inject] private ISubscriber<CellDeselected> _cellDeselected;
        [Inject] private ISubscriber<PlaceBuildingAllowed> _placeBuildingAllowed;
        [Inject] private ISubscriber<PlaceBuildingNotAllowed> _placeBuildingNotAllowed;
        [Inject] private ISubscriber<SelectedBuildingChanged> _selectedBuildingChanged;
        [Inject] private ISubscriber<SelectedCellChanged> _selectedCellChanged;
        [Inject] private BuildingGhostView _view;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _selectedCellChanged.Subscribe(x => UpdateGhostPosition(x)).AddTo(_disposable);
            _selectedBuildingChanged.Subscribe(x => UpdateGhostModel(x)).AddTo(_disposable);
            _cellDeselected.Subscribe(x => { SetGhostActive(false); }).AddTo(_disposable);
            _placeBuildingAllowed.Subscribe(x => SetGhostBuildingAllowed(true)).AddTo(_disposable);
            _placeBuildingNotAllowed.Subscribe(x => SetGhostBuildingAllowed(false)).AddTo(_disposable);
        }

        private void SetGhostActive(bool active)
        {
            _view.SetActive(active);
        }

        private void UpdateGhostModel(SelectedBuildingChanged selectedBuildingChanged)
        {
            var buildingConfig = _buildingsRepository.GetBuildingConfigOfType(
                selectedBuildingChanged.NewSelectedBuildingType);

            var model = buildingConfig.Prefab.transform.GetChild(0).gameObject;

            _view.ChangeModel(model);
        }

        private void SetGhostBuildingAllowed(bool buildingAllowed) =>
            _view.SetBuildingAllowed(buildingAllowed);

        private void UpdateGhostPosition(SelectedCellChanged selectedCellChanged)
        {
            SetGhostActive(true);
            _view.UpdatePosition(selectedCellChanged.NewSelectedCell.transform.position);
        }
    }
}