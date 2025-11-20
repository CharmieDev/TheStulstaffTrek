using System;
using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private RuntimeDialogueGraph dialogueGraph;
    [SerializeField] private UnityEngine.GameObject chaseTrigger;


    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            AudioManager.Instance.StopSong();
            CaseManager.Instance.AudioParentObject.SetActive(false);
            BellRandomizer.Instance.BellActive = false;
            InputManager.Instance.DisableAllInput();

            StartCoroutine(DelayDialogue());
            
            chaseTrigger.SetActive(true);

            
        }
    }

    public IEnumerator DelayDialogue()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.ChangeGameState(GameState.Dialogue);
        DialogueManager.Instance.StartDialogue(dialogueGraph);
        gameObject.SetActive(false);
    }
}
