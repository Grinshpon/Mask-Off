using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MoveState
{
  float movementDeadzone = 0.5f;

  public override void FixedTick()
  {
    UpdateVars();
    HandleJump();
    HandleCounterMovement();

    if (rb.velocity.sqrMagnitude < movementDeadzone)
    {
      //rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
      rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
    }
  }
}
