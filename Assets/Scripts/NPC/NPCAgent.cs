using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAgent : MonoBehaviour
{
  NavMeshAgent agent;
  Animator animator;
  Transform myTransform;
  AudioSource footsteps;

  PatrolState patrolState;
  SearchState searchState;
  AlertState alertState;
  public StateMachine npcSM;

  public float suspicionLevel;

  protected int vertical;
  //[SerializeField]
  //public Transform target; //the player's transform

  void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
    animator = GetComponentInChildren<Animator>();
    myTransform = transform;
    footsteps = GetComponent<AudioSource>();

    patrolState = GetComponent<PatrolState>();
    searchState = GetComponent<SearchState>();
    alertState = GetComponent<AlertState>();
    npcSM = new StateMachine();

    vertical = Animator.StringToHash("Vertical");
  }

  void Start()
  {
    npcSM.ChangeState(patrolState);
    footsteps.Play();
    footsteps.Pause();
  }

  void Update()
  {
    if (suspicionLevel > 66f)
    {
      npcSM.ChangeState(alertState);
    }
    else if (suspicionLevel > 33f)
    {
      npcSM.ChangeState(searchState);
    }
    else
    {
      npcSM.ChangeState(patrolState);
    }
    npcSM.Tick();
  }

  void LateUpdate()
  {
    if (animator != null)
    {
      float val = agent.velocity.magnitude/4f;
      //Debug.Log(val);
      animator.SetFloat(vertical, val);
      if (val > 0f)
      {
        footsteps.UnPause();
      }
      else
      {
        footsteps.Pause();
      }
    }
  }
}
