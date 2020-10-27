using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : NPCState
{
  public override void Enter()
  {
    capsule.enabled = false;
    agent.enabled = false;
    foreach(Rigidbody rb in ragdollBodies)
    {
      rb.mass = 0.01f;
      rb.detectCollisions = true;
      rb.isKinematic = false;
    }
    animator.enabled = false;
  }
}
