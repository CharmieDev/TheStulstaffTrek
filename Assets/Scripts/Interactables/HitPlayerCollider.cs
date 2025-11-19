using UnityEngine;

public class HitPlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            CaseManager.Instance.ChangePhase(CasePhase.LoseEnding);
            Destroy(gameObject);
        }
    }
}
