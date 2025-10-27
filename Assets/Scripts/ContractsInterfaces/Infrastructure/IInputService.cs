using UnityEngine;

namespace Infrastructure
{
    public interface IInputService
    {
        Vector2 MousePosition { get; }
        void Enable();
        void Disable();
    }
}