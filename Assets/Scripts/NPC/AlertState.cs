using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : NPCState
{
  public float attackDistance = 1f;
  float oldFovAngle;
  public float alertFovAngle = 90f;

  public override void Enter()
  {
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
      Debug.Log("I attack thee!");
    }

    if (!vision.seePlayer)
    {
      DeescalateSuspicion();
    }
  }
}
