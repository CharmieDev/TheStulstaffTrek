using System;
using UnityEngine;

public class ScareTrigger : MonoBehaviour
{
    [SerializeField] private UnityEngine.GameObject DrKaelChase;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            CaseManager.Instance.ChaseKael = Instantiate(DrKaelChase, spawnPoint.position, spawnPoint.rotation);
            gameObject.SetActive(false);
        }
    }
}
