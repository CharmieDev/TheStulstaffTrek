using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class InterrogateTrigger : MonoBehaviour, IInteractable
{
    public bool CanInteract { get; set; }
    
    public bool PlayerInsideTrigger { get; private set; }

    private float debounce;
    
    
    private void Update()
    {
        debounce -= Time.deltaTime;
        if (debounce > 0f) return;
        
        if (InterrogationRoomManager.Instance.CurrentSuspect == null) return;

        if (InterrogationRoomManager.Instance.CurrentSuspect.hasInterrogated) return;
        
        if (InterrogationRoomManager.Instance.CurrentSuspect.StateMachine.CurrentState == InterrogationRoomManager.Instance.CurrentSuspect.TalkingState) return;
        
        TextMeshProUGUI interactPromptText = InterrogationRoomManager.Instance.CurrentSuspect.interactPromptText;
        
        if (PlayerInsideTrigger)
        {
            CanInteract = true;
            if (!interactPromptText.transform.parent.gameObject.activeInHierarchy)
            {
                interactPromptText.text = "Interrogate\n[E]";
                AudioManager.Instance.InteractSFX.PlayFeedbacks();
                interactPromptText.transform.parent.gameObject.SetActive(true); 
            }
        }
        else
        {
            if (interactPromptText.transform.parent.gameObject.activeInHierarchy)
            {
                interactPromptText.transform.parent.gameObject.SetActive(false);
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

        if (!CanInteract) return;
        
        TextMeshProUGUI interactPromptText = InterrogationRoomManager.Instance.CurrentSuspect.interactPromptText;
        interactPromptText.transform.parent.gameObject.SetActive(false);
        debounce = 0.5f;
        InterrogationRoomManager.Instance.StartInterrogation();
    }
}