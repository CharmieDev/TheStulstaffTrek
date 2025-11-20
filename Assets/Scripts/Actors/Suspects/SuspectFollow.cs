using System;
using MoreMountains.Feedbacks;
using UnityEngine;

[Serializable]
public class SuspectFollow : State<Suspect>
{
    private PlayerController player;
    private float lerpSpeed = 5f;
    [SerializeField] private float followDistance = 1.5f;
    [SerializeField] private float footstepTime = 0.4f;
    [SerializeField] private MMF_Player footsteps;
    private float timer;
    public override void EnterState()
    {
        base.EnterState();
        if (Context.Animator != null)
        {
            Context.Animator.Play("Walk");
        }
        Context.agent.enabled = true;
        player = PlayerController.Instance;
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.agent.enabled = false;
        
    }

    public override void UpdateState()
    {
        base.UpdateState();
        float dist = Vector3.Distance(Context.transform.position, player.transform.position);

        if (dist > followDistance)
        {
            Context.agent.SetDestination(player.transform.position);
        }
        else
        {
            Context.agent.ResetPath(); // stop jittering
        }
        
        Vector3 playerPos = new Vector3(PlayerController.Instance.transform.position.x, 0, PlayerController.Instance.transform.position.z);
        Vector3 actorPos = new Vector3(Context.transform.position.x, 0, Context.transform.position.z);
        Vector3 direction = (playerPos - actorPos).normalized;
        if (Context.Animator != null)
        {
            Context.Animator.transform.forward = Vector3.Lerp(Context.Animator.transform.forward, direction, lerpSpeed * Time.deltaTime);
        }


        timer -= Time.deltaTime;

        if (timer < 0)
        {
            footsteps.PlayFeedbacks();
            timer = footstepTime;
        }

    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }
    
}