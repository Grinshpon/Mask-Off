using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAgent : MonoBehaviour
{
  NavMeshAgent agent;
  Animator animator;
  Transform myTransform;

  [SerializeField]
  PatrolPath patrolPath;
  [SerializeField]
  float stopTime;

  int pathIndex;
  bool stopped;

  public float suspicionLevel;

  static int vertical;
  //static int horizontal;

  void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
    animator = GetComponent<Animator>();
    myTransform = transform;

    vertical = Animator.StringToHash("Vertical");
  }

  void Start()
  {
    stopped = false;
    pathIndex = 0;
    agent.SetDestination(patrolPath.buoys[0].position);
  }

  void Update()
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

    if (animator != null)
    {
      float val = agent.velocity.magnitude/4f;
      //Debug.Log(val);
      animator.SetFloat(vertical, val);
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
