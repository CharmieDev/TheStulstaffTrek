using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HoldCellTrigger : MonoBehaviour, IInteractable
{
    public bool CanInteract { get; set; }
    [SerializeField] private RuntimeDialogueGraph moreWitnessesDialogue;
    [FormerlySerializedAs("gameObject")] [SerializeField] private Suspect suspect;
    [SerializeField] private RuntimeDialogueGraph otherInterrogateDialogue;
    [FormerlySerializedAs("interactPromptObject")] [SerializeField] private TextMeshProUGUI interactPromptText;
    
    public bool PlayerInsideTrigger { get; private set; }

    private float debounce;

    private void Start()
    {
        interactPromptText.transform.parent.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        debounce -= Time.deltaTime;
        if (debounce > 0f) return;

        if (suspect.StateMachine.CurrentState == suspect.TalkingState) return;

        if (GameManager.Instance.CurrentGameState == GameState.Dialogue) return;
        
        if (!(CaseManager.Instance.CurrentPhase == CasePhase.HoldingCell 
              || CaseManager.Instance.CurrentPhase == CasePhase.Witness 
              || CaseManager.Instance.CurrentPhase == CasePhase.Judgement)) return;

        if (PlayerInsideTrigger)
        {
            CanInteract = true;
            if (!interactPromptText.transform.parent.gameObject.activeInHierarchy)
            {
                interactPromptText.text = suspect.SuspectName + "\n[E]";
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
        
        debounce = 0.5f;
        interactPromptText.transform.parent.gameObject.SetActive(false);
        if(CaseManager.Instance.CurrentPhase == CasePhase.HoldingCell && !suspect.hasInterrogated)
            suspect.TalkInHoldingCell();
        else if(CaseManager.Instance.CurrentPhase == CasePhase.HoldingCell && suspect.hasInterrogated)
            OtherInterrogateDialogue();
        else if (CaseManager.Instance.CurrentPhase == CasePhase.Judgement)
            suspect.JudgementTalk();
        else if(CaseManager.Instance.CurrentPhase == CasePhase.Witness)
            ShowWitnessesDialogue();
    }

    private void ShowWitnessesDialogue()
    {
        GameManager.Instance.ChangeGameState(GameState.Dialogue);
        DialogueManager.Instance.StartDialogue(moreWitnessesDialogue);
    }

    private void OtherInterrogateDialogue()
    {
        GameManager.Instance.ChangeGameState(GameState.Dialogue);
        DialogueManager.Instance.StartDialogue(otherInterrogateDialogue);
    }
}