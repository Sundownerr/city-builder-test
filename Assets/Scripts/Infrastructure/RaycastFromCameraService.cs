using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure
{
    public class RaycastFromCameraService : ITickable, IRaycastFromCameraService
    {
        [Inject] private Camera _camera;
        [Inject] private IInputService _inputService;

        public Vector3 RayOrigin { get; private set; }
        public Vector3 RayDirection { get; private set; }
        public Ray Ray { get; private set; }

        public void Tick()
        {
            var cameraScreenPointToRay = _camera.ScreenPointToRay(_inputService.MousePosition);

            Ray = new Ray(_camera.transform.position, cameraScreenPointToRay.direction);

            RayOrigin = Ray.origin;
            RayDirection = Ray.direction;
        }
    }
}