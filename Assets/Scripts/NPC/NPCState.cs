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
  protected CapsuleCollider capsule;

  protected Rigidbody[] ragdollBodies;

  protected int vertical;

  [SerializeField]
  public float deescalateRate = 1f;

  void Awake()
  {
    guard = GetComponent<NPCAgent>();
    vision = GetComponentInChildren<NPCVision>();
    agent = GetComponent<NavMeshAgent>();
    animator = GetComponentInChildren<Animator>();
    myTransform = GetComponent<Transform>();
    capsule = GetComponent<CapsuleCollider>();

    ragdollBodies = GetComponentsInChildren<Rigidbody>();

    vertical = Animator.StringToHash("Vertical");
  }

  void Start()
  {
    foreach(Rigidbody rb in ragdollBodies)
    {
      rb.isKinematic = true;
      rb.detectCollisions = false;
    }
  }

  protected void DeescalateSuspicion()
  {
    guard.suspicionLevel = Mathf.Clamp(guard.suspicionLevel - (deescalateRate * Time.deltaTime), 0f, 100f);
  }

}
