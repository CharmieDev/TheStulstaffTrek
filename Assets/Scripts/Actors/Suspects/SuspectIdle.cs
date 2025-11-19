using System;
using UnityEngine;

[Serializable]
public class SuspectIdle : State<Suspect>
{
    public override void EnterState()
    {
        base.EnterState();
        if (Context.Animator != null)
        {
            Context.Animator.Play("Idle");
        }
    }

    public void WitnessManager_OnTalkToWitness()
    {
        IsComplete = true;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }
    
}