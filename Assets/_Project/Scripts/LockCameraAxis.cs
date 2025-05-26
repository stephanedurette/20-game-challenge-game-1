using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
/// </summary>
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class LockCameraAxis : CinemachineExtension
{

    public bool lockXPosition;
    public bool lockYPosition;
    public bool lockZPosition;

    private Vector3 startPosition;

    protected void OnValidate()
    {
        startPosition = transform.position;
    }

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;

            if (lockXPosition) pos.x = startPosition.x;
            if (lockYPosition) pos.y = startPosition.y;
            if (lockZPosition) pos.z = startPosition.z;

            state.RawPosition = pos;
        }
    }
}
