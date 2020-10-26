using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Suspicious State
public class SearchState : NPCState
{
  SearchArea searchArea;

  bool movingToPoint;
  bool stopped;

  [SerializeField]
  public float stopTime = 3f;

  public override void Enter()
  {
    // currently searchArea only defined by last known position.
    // When sound stuff is implemented it will be based on whether
    // or not the guard heard or saw the player, and if he only heard
    // something he'll begin it at his own position;
    searchArea = new SearchArea(vision.lastKnownPosition, 5f);
    movingToPoint = false;
    stopped = false;
    Debug.Log("What was that?");
  }

  public override void Tick()
  {
    if (!movingToPoint)
    {
      Vector3 nextPoint = searchArea.RandomPoint();
      Debug.Log("Moving to " + nextPoint);
      agent.SetDestination(nextPoint);
      movingToPoint = true;
      stopped = false;
    }
    else if (!stopped)
    {
      if (agent.remainingDistance <= agent.stoppingDistance)
      {
        stopped = true;
        StartCoroutine(LookThenCont());
      }
    }

    guard.suspicionLevel -= deescalateRate * Time.deltaTime;
  }

  IEnumerator LookThenCont()
  {
    float timer = 0f;
    while(timer < stopTime) // prematurely end this if guard is suspicious or alerted
    {
      timer += Time.deltaTime;
      yield return null;
    }
    movingToPoint = false;
    stopped = false;
    yield return null;
  }
}
