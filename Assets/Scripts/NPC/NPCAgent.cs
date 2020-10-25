using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAgent : MonoBehaviour
{
  NavMeshAgent agent;

  [SerializeField]
  PatrolPath patrolPath;
  [SerializeField]
  float stopTime;

  int pathIndex;
  bool stopped;

  void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
  }

  void Start()
  {
    stopped = false;
    pathIndex = 0;
    agent.SetDestination(patrolPath.buoys[0].position);
  }

  void Update()
  {
    if (agent.remainingDistance == 0)
    {
      if (!stopped)
      {
        stopped = true;
        StartCoroutine(StopThenCont());
      }
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
