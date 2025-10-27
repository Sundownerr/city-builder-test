using System;
using Domain.Application.MessagesDTO;
using MessagePipe;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Infrastructure
{
    public class InputService : IInitializable, IDisposable, IInputService, ITickable
    {
        private InputSystem_Actions _inputSystemActions;
        [Inject] private IPublisher<SelectBuilding1Pressed> _selectBuilding1PressedPublisher;
        [Inject] private IPublisher<SelectBuilding2Pressed> _selectBuilding2PressedPublisher;
        [Inject] private IPublisher<SelectBuilding3Pressed> _selectBuilding3PressedPublisher;
        [Inject] private IPublisher<SelectPressed> _selectPressedPublisher;
        public Vector2 MousePosition { get; private set; }

        public void Dispose()
        {
            _inputSystemActions.Player.SelectBuilding1.performed -= SendSelectBuilding1;
            _inputSystemActions.Player.SelectBuilding2.performed -= SendSelectBuilding2;
            _inputSystemActions.Player.SelectBuilding3.performed -= SendSelectBuilding3;
            _inputSystemActions.Player.Select.performed -= SendSelect;
        }

        public void Initialize()
        {
            _inputSystemActions = new InputSystem_Actions();
            _inputSystemActions.Enable();

            _inputSystemActions.Player.SelectBuilding1.performed += SendSelectBuilding1;
            _inputSystemActions.Player.SelectBuilding2.performed += SendSelectBuilding2;
            _inputSystemActions.Player.SelectBuilding3.performed += SendSelectBuilding3;
            _inputSystemActions.Player.Select.performed += SendSelect;
        }

        public void Enable() =>
            _inputSystemActions.Enable();

        public void Disable() =>
            _inputSystemActions.Disable();

        public void Tick() =>
            MousePosition = _inputSystemActions.Player.Mouse.ReadValue<Vector2>();

        private void SendSelectBuilding1(InputAction.CallbackContext obj) =>
            _selectBuilding1PressedPublisher.Publish(new SelectBuilding1Pressed());

        private void SendSelectBuilding2(InputAction.CallbackContext obj) =>
            _selectBuilding2PressedPublisher.Publish(new SelectBuilding2Pressed());

        private void SendSelectBuilding3(InputAction.CallbackContext obj) =>
            _selectBuilding3PressedPublisher.Publish(new SelectBuilding3Pressed());

        private void SendSelect(InputAction.CallbackContext obj) =>
            _selectPressedPublisher.Publish(new SelectPressed());
    }
}