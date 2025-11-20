using System;
using UnityEngine;

[Serializable]
public class WitnessExit : State<Witness>
{
    [SerializeField] private float moveSpeed = 2f, arrivalDistance = 0.25f;
    private float lerpSpeed = 5f;
    private Transform[] currentWaypoints;
    private int currentWaypoint;
    
    public override void EnterState()
    {
        base.EnterState();
        Context.Footsteps.PlayFeedbacks();
        
        if (Context.Animator != null)
        {
            Context.Animator.Play("Walk");
        }
        currentWaypoint = 0;
        currentWaypoints = GetWaypoints();
    }
    public virtual Transform[] GetWaypoints()
    {
        return WitnessManager.Instance.ExitWaypoints;
    }
    public override void ExitState()
    {
        base.ExitState();
        Context.Footsteps.StopFeedbacks();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Vector3 waypointPos = new Vector3(currentWaypoints[currentWaypoint].position.x, 0, currentWaypoints[currentWaypoint].position.z);
        Vector3 actorPos = new Vector3(Context.transform.position.x, 0, Context.transform.position.z);
        Vector3 direction = (waypointPos - actorPos).normalized;
        Context.Rb.linearVelocity = direction * moveSpeed;
        if (Context.Animator != null)
        {
            Context.Animator.transform.forward = Vector3.Lerp(Context.Animator.transform.forward, direction, lerpSpeed * Time.deltaTime);
        }
        Debug.DrawRay(Context.transform.position, direction * moveSpeed, Color.red);
        if (Vector3.Distance(actorPos, waypointPos) < arrivalDistance)
        {
            if (currentWaypoint == currentWaypoints.Length - 1)
            {
                IsComplete = true;
            }
            else
            {
                currentWaypoint++;
            }
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

}