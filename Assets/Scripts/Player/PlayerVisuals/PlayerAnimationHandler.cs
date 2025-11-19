using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private Animator animator;
    public void Start()
    {
        animator = player.Animator;
        player.StateMachine.OnStateChanged += OnStateChanged;
    }

    public void Update()
    {
    }

    /// <summary>
    /// This method is called when a new state is entered
    /// </summary>
    private void OnStateChanged(object sender, StateMachine<PlayerController>.OnStateChangedEventArgs eventArgs)
    {
        SetTrigger(getTriggerName(eventArgs.NextState));
    }

    private String getTriggerName(State<PlayerController> state)
    {
        
        if (state == player.MoveState)
        {
            return "Move";
        }
        if (state == player.IdleState)
        {
            return "Attack";
        }


        return "";
    }
    
    /// <summary>
    /// Reset all animation triggers. If a new trigger is added to the animator, it needs to be reset in this function.
    /// </summary>
    public void ResetAllTriggers()
    {
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Wallrun");
        animator.ResetTrigger("Idle");
    }

    /// <summary>
    /// Set a trigger in the player's animator
    /// </summary>
    /// <param name="trigger"></param>
    public void SetTrigger(string trigger)
    {
        ResetAllTriggers();
        animator.SetTrigger(trigger);
    }
}
