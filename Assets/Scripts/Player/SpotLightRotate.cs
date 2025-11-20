using UnityEngine;

public class SpotLightRotate : MonoBehaviour
{
    [SerializeField] private Transform LookTransform;
    [SerializeField] private float lerpSpeed = 1f;
    
    // Update is called once per frame
    void Update()
    {
        // Smooth rotation toward LookTransform
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            LookTransform.rotation,
            lerpSpeed * Time.deltaTime
        );
    }
}
