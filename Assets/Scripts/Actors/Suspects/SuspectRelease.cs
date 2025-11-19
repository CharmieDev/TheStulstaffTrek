using System;
using UnityEngine;

[Serializable]
public class SuspectRelease : State<Suspect>
{
    [SerializeField] private float fadeInTime = 0.5f;
    [SerializeField] private float fadeOutTime = 0.5f;

    [SerializeField] private Transform teleportPoint;
    
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
        
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        if (StateUptime > fadeInTime && !hasMoved)
        {
            Context.transform.position = teleportPoint.position;
            hasMoved = true;

            // Skip fade out
            if (fadeOutTime < 0)
            {
                IsComplete = true;
                return;
            }
            
            UIManager.Instance.FadeOut(fadeOutTime);
        }

        if (StateUptime > fadeInTime + fadeOutTime)
        {
            IsComplete = true;
            GameManager.Instance.ChangeGameState(GameState.Gameplay);
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }
}