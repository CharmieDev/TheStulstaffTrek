using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class WitnessTalking : State<Witness>
{
    private float lerpSpeed = 5f;
    public override void EnterState()
    {
        base.EnterState();
        if (Context.Animator != null)
        {
            Context.Animator.Play("Idle");
        }
        DialogueManager.Instance.OnDialogueEnd.AddListener(DialogueManager_OnDialogueEnd);
        GameManager.Instance.ChangeGameState(GameState.Dialogue);
        CameraManager.Instance.SetDirectedCamera(Context.LookAt);
        DialogueManager.Instance.StartDialogue(Context.DialogueGraph);

    }

    public override void ExitState()
    {
        base.ExitState();
        DialogueManager.Instance.OnDialogueEnd.RemoveListener(DialogueManager_OnDialogueEnd);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Vector3 playerPos = new Vector3(PlayerController.Instance.transform.position.x, 0, PlayerController.Instance.transform.position.z);
        Vector3 actorPos = new Vector3(Context.transform.position.x, 0, Context.transform.position.z);
        Vector3 direction = (playerPos - actorPos).normalized;
        if (Context.Animator != null)
        {
            Context.Animator.transform.forward = Vector3.Lerp(Context.Animator.transform.forward, direction, lerpSpeed * Time.deltaTime);
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

    public void DialogueManager_OnDialogueEnd()
    {
        IsComplete = true;
    }
}