using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;



[Serializable]
public struct WitnessPhase
{
    public WitnessInformation[] witnesses;
}
[Serializable]
public struct WitnessInformation
{
    public UnityEngine.GameObject witnessPrefab;
}
public class WitnessManager : Singleton<WitnessManager>
{
    [Header("Waypoints")]
    [field: SerializeField] public Transform[] EnterWaypoints { get; private set; }
    [field: SerializeField] public Transform[] ExitWaypoints { get; private set; }
    [SerializeField] private Transform witnessSpawnPoint;

    [Header("Witness")]
    [field:SerializeField] public WitnessPhase[] WitnessPhases { get; private set; }
    [field:SerializeField] public Witness CurrentWitness { get; private set; }
    [field:SerializeField] public UnityEvent OnTalkToWitness { get; private set; }

    [Header("Phase Properties")]
    [field: SerializeField] public int currentPhaseIndex { get; private set; } = 0;
    [field: SerializeField] public int currentWitnessIndex { get; private set; } = 0;

    [SerializeField] private float spawnDelay;
    
    
    [Button]
    public void StartNextWitnessPhase()
    {
        if (CaseManager.Instance.CurrentPhase == CasePhase.Witness) return;
        
        CaseManager.Instance.ChangePhase(CasePhase.Witness);
        SpawnNextWitness();
    }

    public void EndWitnessPhase()
    {
        CaseManager.Instance.ChangePhase(CasePhase.HoldingCell);
        currentPhaseIndex++;
        currentWitnessIndex = 0;
    }

    public void Update()
    {
        if (!(CaseManager.Instance.CurrentPhase == CasePhase.Witness && CurrentWitness == null)) return;


        // Ran out of witnesses for this stage
        if (currentWitnessIndex == WitnessPhases[currentPhaseIndex].witnesses.Length)
        {
            EndWitnessPhase();
        }
        else
        {
            SpawnNextWitness();
        }
    }
    public void SpawnNextWitness()
    {
        CurrentWitness = Instantiate(GetCurrentWitnessInformation().witnessPrefab, witnessSpawnPoint.position, Quaternion.identity, transform).GetComponent<Witness>();
        currentWitnessIndex++;
    }
    
    public void TryTalkingToWitness()
    {
        Debug.Log("Talking to witness");
        OnTalkToWitness?.Invoke();
    }

    public WitnessInformation GetCurrentWitnessInformation()
    {
        return WitnessPhases[currentPhaseIndex].witnesses[currentWitnessIndex];
    }



    public void DestroyWitness()
    {
        Destroy(CurrentWitness.gameObject);
        CurrentWitness = null;
    }
    
}
