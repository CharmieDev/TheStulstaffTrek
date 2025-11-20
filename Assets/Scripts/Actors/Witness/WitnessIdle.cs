using System;
using UnityEngine;

[Serializable]
public class WitnessIdle : State<Witness>
{
    public override void EnterState()
    {
        base.EnterState();
        if (Context.Animator != null)
        {
            Context.Animator.Play("Idle");
        }
        WitnessManager.Instance.OnTalkToWitness.AddListener(WitnessManager_OnTalkToWitness);
        BellRandomizer.Instance.BellActive = true;
    }

    public void WitnessManager_OnTalkToWitness()
    {
        IsComplete = true;
    }

    public override void ExitState()
    {
        base.ExitState();
        BellRandomizer.Instance.BellActive = false;
        WitnessManager.Instance.OnTalkToWitness.RemoveListener(WitnessManager_OnTalkToWitness);
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