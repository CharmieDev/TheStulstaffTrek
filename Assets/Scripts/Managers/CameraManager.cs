using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public PlayerCamera PlayerCamera;
    public CinemachineVirtualCameraBase DirectedCamera;
    
    public void SetDirectedCamera(Transform target)
    {
        if (PlayerCamera == null) return;
        
        
        DirectedCamera.transform.position = PlayerCamera.transform.position;
        DirectedCamera.LookAt = target;
        DirectedCamera.gameObject.SetActive(true);
        PlayerCamera.gameObject.SetActive(false);
    }

    public void SetPlayerCamera()
    {
        if (PlayerCamera == null) return;

        
        // Match the player camera’s transform to where the directed camera currently is
        // PlayerCamera.PanTilt.ForceCameraPosition(PlayerCamera.transform.position, DirectedCamera.transform.rotation);
        
        PlayerCamera.gameObject.SetActive(true);
        DirectedCamera.gameObject.SetActive(false);
    }
}
