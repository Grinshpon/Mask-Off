using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : NPCState
{
  //weapon collider needs a special case because it's a trigger but needs to be not one when
  // the guard dies
  public Collider weapon;

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
    weapon.isTrigger = false;
    animator.enabled = false;
  }
}
