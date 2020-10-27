using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : NPCState
{
  public float attackDistance = 1f;

  public override void Enter()
  {
    guard.alerted.Play();
    agent.speed = 4f;
  }

  public override void Exit()
  {
    agent.speed = 2f;
  }

  public override void Tick()
  {
    agent.SetDestination(vision.lastKnownPosition);

    if (agent.remainingDistance <= attackDistance && vision.seePlayer)
    {
      Debug.Log("I attack thee!");
    }
  }
}
