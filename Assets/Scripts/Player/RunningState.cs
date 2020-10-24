using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : MoveState
{
  public override void FixedTick()
  {
    UpdateVars();
    HandleJump();
    HandleMovement();
    HandleCounterMovement();
  }
}
