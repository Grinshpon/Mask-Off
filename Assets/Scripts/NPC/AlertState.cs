using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : NPCState
{
  public float attackDistance = 1f;
  float oldFovAngle;
  public float alertFovAngle = 90f;
  bool attacking;

  public override void Enter()
  {
    attacking = false;
    guard.alerted.Play();
    agent.speed = 4f;
    oldFovAngle = vision.angle;
    vision.angle = alertFovAngle;
  }

  public override void Exit()
  {
    agent.speed = 2f;
    vision.angle = oldFovAngle;
  }

  public override void Tick()
  {
    agent.SetDestination(vision.lastKnownPosition);

    if (agent.remainingDistance <= attackDistance && vision.seePlayer)
    {
      Attack();
      //Debug.Log("I attack thee!");
    }

    if (!vision.seePlayer)
    {
      DeescalateSuspicion();
    }
  }

  void Attack()
  {
    if (!attacking)
    {
      StartCoroutine(DoAttack());
    }
  }

  IEnumerator DoAttack()
  {
    attacking = true;
    bool hitPlayer = false;
    agent.isStopped = true;
    animator.SetBool("isInteracting", true);
    //animator.applyRootMotion = true;
    animator.CrossFade("Attack", 0.2f);
    while (animator.GetBool("isInteracting"))
    {
      // make sure player only gets "hit" once per attack cycle
      if (!hitPlayer)
      {
        if (guard.weapon.touchingPlayer)
        {
          //Debug.Log("OUCH");
          guard.target.stats.DecHealth(20f);
          hitPlayer = true;
        }
      }
      yield return null;
    }
    //animator.applyRootMotion = false;
    agent.isStopped = false;
    attacking = false;
    yield return null;
  }
}
