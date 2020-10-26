using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCState : State
{
  protected NPCAgent guard;
  protected NavMeshAgent agent;
  protected Animator animator;
  protected Transform myTransform;

  protected int vertical;

  void Awake()
  {
    guard = GetComponent<NPCAgent>();
    agent = GetComponent<NavMeshAgent>();
    animator = GetComponentInChildren<Animator>();
    myTransform = GetComponent<Transform>();

    vertical = Animator.StringToHash("Vertical");
  }

}
