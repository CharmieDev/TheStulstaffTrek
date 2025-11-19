using System;
using MoreMountains.Feedbacks;
using UnityEngine;

[Serializable]
public class PlayerMove : State<PlayerController>
{
    public float MoveSpeed;
    public override void EnterState()
    {
        base.EnterState();
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

        Vector2 inputVector = Context.InputManager.MoveVector;
        Vector3 moveDir = Context.Orientation.forward * inputVector.y + Context.Orientation.right * inputVector.x;
        Context.Rb.AddForce(moveDir * MoveSpeed, ForceMode.VelocityChange);

    }
}
