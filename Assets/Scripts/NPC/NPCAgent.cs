using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAgent : MonoBehaviour
{
  NavMeshAgent agent;
  Animator animator;
  Transform myTransform;

  PatrolState patrolState;
  SearchState searchState;
  AlertState alertState;
  DeadState deadState;
  public StateMachine npcSM;

  public float suspicionLevel;

  protected int vertical;

  public bool alive = true;

  [SerializeField]
  public PlayerController target;
  [SerializeField]
  public WeaponTrigger weapon;

  [Header("Audio Sources")]
  [SerializeField]
  public AudioSource footsteps;
  [SerializeField]
  public AudioSource calmDown;
  [SerializeField]
  public AudioSource suspicious;
  [SerializeField]
  public AudioSource alerted;

  void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
    animator = GetComponentInChildren<Animator>();
    myTransform = transform;

    patrolState = GetComponent<PatrolState>();
    searchState = GetComponent<SearchState>();
    alertState = GetComponent<AlertState>();
    deadState = GetComponent<DeadState>();
    npcSM = new StateMachine();

    vertical = Animator.StringToHash("Vertical");
  }

  void Start()
  {
    alive = true;
    npcSM.ChangeState(patrolState);
    footsteps.Play();
    footsteps.Pause();
  }

  void Update()
  {
    if (alive)
    {
      if (suspicionLevel > 70f)
      {
        npcSM.ChangeState(alertState);
      }
      else if (suspicionLevel > 25f)
      {
        npcSM.ChangeState(searchState);
      }
      else
      {
        npcSM.ChangeState(patrolState);
      }
    }
    else
    {
      npcSM.ChangeState(deadState);
    }
    npcSM.Tick();
  }

  void LateUpdate()
  {
    if (suspicionLevel < 0) suspicionLevel = 0;
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

  public void ReceiveAttack()
  {
    //basically, for now only sneak attacks will kill a guard.
    if (npcSM.currentState is AlertState) return;
    alive = false;
  }
}
