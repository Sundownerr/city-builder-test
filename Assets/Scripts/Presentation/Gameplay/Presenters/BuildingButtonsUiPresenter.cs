using System;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using Presentation.Gameplay.Views;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Presentation.Gameplay.Presenters
{
    public class BuildingButtonsUiPresenter : IStartable, IDisposable
    {
        private Button _farmButton;
        private Button _houseButton;
        private Button _mineButton;
        [Inject] private IPublisher<SelectFarmPressed> _selectFarmPressed;
        [Inject] private IPublisher<SelectHousePressed> _selectHousePressed;
        [Inject] private IPublisher<SelectMinePressed> _selectMinePressed;
        [Inject] private BuildingButtonsUiView _view;

        public void Dispose()
        {
            _houseButton.clicked -= SendHouseClicked;
            _farmButton.clicked -= SendFarmClicked;
            _mineButton.clicked -= SendMineClicked;
        }

        public void Start()
        {
            var root = _view.UIDocument.rootVisualElement;

            _houseButton = root.Q<Button>("HouseButton");
            _farmButton = root.Q<Button>("FarmButton");
            _mineButton = root.Q<Button>("MineButton");

            _houseButton.clicked += SendHouseClicked;
            _farmButton.clicked += SendFarmClicked;
            _mineButton.clicked += SendMineClicked;
        }

        private void SendMineClicked() =>
            _selectMinePressed.Publish(new SelectMinePressed());

        private void SendFarmClicked() =>
            _selectFarmPressed.Publish(new SelectFarmPressed());

        private void SendHouseClicked() =>
            _selectHousePressed.Publish(new SelectHousePressed());
    }
}