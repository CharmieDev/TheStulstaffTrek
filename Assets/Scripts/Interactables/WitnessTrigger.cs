using System;
using UnityEngine;
using UnityEngine.Serialization;

public class WitnessTrigger : MonoBehaviour, IInteractable
{
    public bool CanInteract { get; set; }

    public bool PlayerInsideTrigger { get; private set; }

    private float debounce;

    private void Update()
    {
        debounce -= Time.deltaTime;
        if (debounce > 0f) return;
        
        Witness currentWitness = WitnessManager.Instance.CurrentWitness;
        if (currentWitness == null) return;
        UnityEngine.GameObject interactPromptObject = currentWitness.InteractObject;

        if (currentWitness.StateMachine.CurrentState != currentWitness.IdleState) return;

        if (PlayerInsideTrigger)
        {
            CanInteract = true;
            if (!interactPromptObject.activeInHierarchy) 
            {
                AudioManager.Instance.InteractSFX.PlayFeedbacks();
                interactPromptObject.SetActive(true); 
            }
        }
        else
        {
            if (interactPromptObject.activeInHierarchy)
            {
                interactPromptObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            PlayerInsideTrigger = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            PlayerInsideTrigger = false;
        }
    }

    public void Interact()
    {
        if (CanInteract)
        {
            WitnessManager.Instance.CurrentWitness.InteractObject.SetActive(false);
            CanInteract = false;
            debounce = 0.5f;
            WitnessManager.Instance.TryTalkingToWitness();
        }
    }
}