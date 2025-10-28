using System;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using Presentation.Gameplay.Views;
using R3;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public class GameplayUiPresenter : IStartable, IDisposable
    {
        [Inject] private ISubscriber<BuildingDeselected> _buildingDeselected;
        private VisualElement _buildingPanel;
        private readonly CompositeDisposable _disposable = new();
        private Button _farmButton;
        private Button _houseButton;
        private Button _mineButton;
        private Button _moveBuildingButton;
        private Button _removeBuildingButton;
        [Inject] private ISubscriber<SelectedBuildingChanged> _selectedBuildingChanged;
        [Inject] private IPublisher<SelectFarmPressed> _selectFarmPressed;
        [Inject] private IPublisher<SelectHousePressed> _selectHousePressed;
        [Inject] private IPublisher<SelectMinePressed> _selectMinePressed;
        private Button _upgradeBuildingButton;
        [Inject] private GameplayUiView _view;

        public void Dispose()
        {
            _houseButton.clicked -= SendHouseClicked;
            _farmButton.clicked -= SendFarmClicked;
            _mineButton.clicked -= SendMineClicked;
            _moveBuildingButton.clicked -= SendMoveBuildingPressed;
            _removeBuildingButton.clicked -= SendRemoveBuildingPressed;
            _upgradeBuildingButton.clicked -= SendUpgradeBuildingPressed;
            
            _disposable.Dispose();
        }

        public void Start()
        {
            var root = _view.UIDocument.rootVisualElement;

            _houseButton = root.Q<Button>("HouseButton");
            _farmButton = root.Q<Button>("FarmButton");
            _mineButton = root.Q<Button>("MineButton");
            _moveBuildingButton = root.Q<Button>("MoveButton");
            _removeBuildingButton = root.Q<Button>("RemoveButton");
            _upgradeBuildingButton = root.Q<Button>("UpgradeButton");
            _buildingPanel = root.Q<VisualElement>("BuildingPanel");

            _houseButton.clicked += SendHouseClicked;
            _farmButton.clicked += SendFarmClicked;
            _mineButton.clicked += SendMineClicked;
            _moveBuildingButton.clicked += SendMoveBuildingPressed;
            _removeBuildingButton.clicked += SendRemoveBuildingPressed;
            _upgradeBuildingButton.clicked += SendUpgradeBuildingPressed;

            _buildingDeselected.Subscribe(x => SetBuildingPanelActive(false)).AddTo(_disposable);
            _selectedBuildingChanged.Subscribe(x => SetBuildingPanelActive(true)).AddTo(_disposable);
            
            SetBuildingPanelActive(false);
        }

        private void SendUpgradeBuildingPressed()
        {
            
        }

        private void SendRemoveBuildingPressed()
        {
            
        }

        private void SendMoveBuildingPressed()
        {
            
        }

        private void SetBuildingPanelActive(bool active) =>
            _buildingPanel.visible = active;

        private void SendMineClicked() =>
            _selectMinePressed.Publish(new SelectMinePressed());

        private void SendFarmClicked() =>
            _selectFarmPressed.Publish(new SelectFarmPressed());

        private void SendHouseClicked() =>
            _selectHousePressed.Publish(new SelectHousePressed());
    }
}