using System;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using Presentation.Gameplay.Views;
using R3;
using Repositories.Gameplay;
using VContainer;
using VContainer.Unity;
using DisposableBag = MessagePipe.DisposableBag;

namespace Presentation.Gameplay.Presenters
{
    public class BuildingGhostPresenter : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        [Inject] private ISubscriber<CellDeselected> _cellDeselected;
        [Inject] private ISubscriber<SelectedCellChanged> _selectedCellChanged;
        [Inject] private ISubscriber<SelectedBuildingChanged> _selectedBuildingChanged;
        [Inject] private BuildingGhostView _view;
        [Inject] private BuildingsRepository _buildingsRepository;

        public void Dispose() =>
            _disposable.Dispose();

        public void Initialize()
        {
            _selectedCellChanged.Subscribe(x => UpdateGhostPosition(x)).AddTo(_disposable);
            _selectedBuildingChanged.Subscribe(x => UpdateGhostModel(x)).AddTo(_disposable);
        }

        private void UpdateGhostModel(SelectedBuildingChanged selectedBuildingChanged)
        {
            var buildingConfig = _buildingsRepository.GetBuildingConfigOfType(
                selectedBuildingChanged.NewSelectedBuildingType);

            var model = buildingConfig.Prefab.transform.GetChild(0).gameObject;
            
            _view.ChangeModel(model);
        }

        private void UpdateGhostPosition(SelectedCellChanged selectedCellChanged)
        {
            _view.UpdatePosition(selectedCellChanged.NewSelectedCell.transform.position);
        }
    }
}