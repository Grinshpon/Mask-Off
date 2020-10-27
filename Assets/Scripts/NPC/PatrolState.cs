using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : NPCState
{
  [SerializeField]
  public PatrolPath patrolPath = null;
  [SerializeField]
  public float stopTime = 3f;

  public int pathIndex;
  public bool stopped;

  void Start()
  {
    stopped = false;
    pathIndex = 0;
  }

  public override void Enter()
  {
    if (patrolPath != null) agent.SetDestination(patrolPath.buoys[pathIndex].position);
    guard.calmDown.Play();
  }

  public override void Tick()
  {
    if (patrolPath != null)
    {
      //Debug.Log(agent.remainingDistance);
      if (agent.remainingDistance <= agent.stoppingDistance)
      {
        if (!stopped)
        {
          stopped = true;
          StartCoroutine(StopThenCont());
        }
      }
    }

    DeescalateSuspicion();
  }

  public override void Exit()
  {
    if (stopped)
    {
      StopCoroutine(StopThenCont());
    }
  }

  IEnumerator StopThenCont()
  {
    stopped = true;
    float timer = 0f;
    while(timer < stopTime) // prematurely end this if guard is suspicious or alerted
    {
      timer += Time.deltaTime;
      yield return null;
    }
    stopped = false;
    if (pathIndex == patrolPath.buoys.Count-1) pathIndex = 0;
    else pathIndex++;

    agent.SetDestination(patrolPath.buoys[pathIndex].position);
    yield return null;
  }
}
