using System;
using UnityEngine;

[Serializable]
public class SuspectInterrogate : State<Suspect>
{
    [SerializeField] private float fadeInTime = 0.5f;
    [SerializeField] private float fadeOutTime = 0.5f;

    private bool hasMoved;

    public override void EnterState()
    {
        base.EnterState();
        if (Context.Animator != null)
        {
            Context.Animator.Play("Idle");
        }
        hasMoved = false;
        GameManager.Instance.ChangeGameState(GameState.Cutscene);
        UIManager.Instance.FadeIn(fadeInTime);
    }

    public override void ExitState()
    {
        base.ExitState();
        GameManager.Instance.ChangeGameState(GameState.Gameplay);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (StateUptime > fadeInTime && !hasMoved)
        {
            InterrogationRoomManager.Instance.MoveToInterrogation(Context);
            hasMoved = true;
            UIManager.Instance.FadeOut(fadeOutTime);
        }

        if (StateUptime > fadeInTime + fadeOutTime)
        {
            IsComplete = true;
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }
}