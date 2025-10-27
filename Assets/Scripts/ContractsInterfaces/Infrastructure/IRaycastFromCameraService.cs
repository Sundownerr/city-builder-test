using UnityEngine;

namespace Infrastructure
{
    public interface IRaycastFromCameraService
    {
        Vector3 RayOrigin { get; }
        Vector3 RayDirection { get; }

        Ray Ray { get; }
    }
}