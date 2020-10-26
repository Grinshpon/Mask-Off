using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCState : State
{
  protected NPCAgent guard;
  protected NavMeshAgent agent;
  protected NPCVision vision;
  protected Animator animator;
  protected Transform myTransform;

  protected int vertical;

  [SerializeField]
  public float deescalateRate = 1f;

  void Awake()
  {
    guard = GetComponent<NPCAgent>();
    vision = GetComponent<NPCVision>();
    agent = GetComponent<NavMeshAgent>();
    animator = GetComponentInChildren<Animator>();
    myTransform = GetComponent<Transform>();

    vertical = Animator.StringToHash("Vertical");
  }

}
