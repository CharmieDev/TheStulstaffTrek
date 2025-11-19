using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotateTime;

    // Update is called once per frame
    void Start()
    {
        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 360, transform.eulerAngles.z),
            rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
}
