using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    [SerializeField] private Transform CameraTransform;

    private void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, CameraTransform.eulerAngles.y, transform.eulerAngles.z);
    }
}
