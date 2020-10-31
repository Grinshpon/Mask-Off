using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  PlayerInputHandler inputHandler;
  CapsuleCollider capsule;
  PlayerCamera cam;
  Visibility visibility;
  AudioSource footsteps;
  Transform myTransform;
  Rigidbody rb;

  //don't use MoveStateMachine
  public StateMachine moveSM;

  // Movement States
  RunningState runningState;
  IdleState idleState;
  MidairState midairState;

  // Lean State
  // doesn't require a StateMachine because it's very simple
  LeanState leanState;

  // Crouch State
  CrouchState crouchState;

  // Attack
  PlayerAttacker playerAttack;

  public CharStats stats;
  public Transform weapon;

  public Vector3 groundNormal = Vector3.up;
  [Header("Flags")]
  public Vector2 movement;
  public bool grounded;
  public bool jumping;
  public bool crouching;
  public float leanDir;
  public bool attacking;
  public bool alive;

  void Awake()
  {
    stats = Instantiate(stats);
    visibility = GetComponent<Visibility>();

    inputHandler = GetComponent<PlayerInputHandler>();
    capsule = GetComponent<CapsuleCollider>();
    cam = GetComponent<PlayerCamera>();
    footsteps = GetComponent<AudioSource>();
    myTransform = transform;
    rb = GetComponent<Rigidbody>();

    runningState = GetComponent<RunningState>();
    idleState = GetComponent<IdleState>();
    midairState = GetComponent<MidairState>();
    moveSM = new StateMachine();

    leanState = GetComponent<LeanState>();
    crouchState = GetComponent<CrouchState>();
    playerAttack = GetComponent<PlayerAttacker>();
  }

  void Start()
  {
    alive = true;
    //StartCoroutine(PrintState());
    moveSM.ChangeState(idleState);
  }

  void Update()
  {
    if (stats.health == 0f)
    {
      if (alive)
      {
        alive = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddRelativeForce(-myTransform.forward, ForceMode.Impulse);
      }
      return;
    }
    attacking = inputHandler.attackInput;
    movement = inputHandler.moveInput;
    jumping = jumping || (grounded && inputHandler.jumpInput);
    if (jumping)
    {
      //Debug.Log("Wanna jump");
      inputHandler.jumpInput = false;
    }

    leanDir = inputHandler.leanInput;
    crouching = inputHandler.crouchInput;

    if (crouching)
    {
      footsteps.pitch = 0.75f;
    }
    else
    {
      footsteps.pitch = 1f;
    }

    if (grounded) {
      if (movement.sqrMagnitude > 0) moveSM.ChangeState(runningState);
      else moveSM.ChangeState(idleState);
    }
    else moveSM.ChangeState(midairState);

    moveSM.Tick();
    leanState.Tick();
    crouchState.Tick();
    playerAttack.Tick();
    visibility.Tick();
  }

  void FixedUpdate()
  {
    if(alive)
    {
      HandleGround();
      moveSM.FixedTick();
    }
  }

  void LateUpdate()
  {
    if(alive)
    {
      moveSM.LateTick();
      cam.HandleRotation(Time.deltaTime);
      cam.HandleBobbing(Time.deltaTime);
    }
  }

  public void HandleGround()
  {
    float radius = capsule.radius * 0.9f;
    //Vector3 pos = myTransform.position - Vector3.up*(capsule.height/2f-radius+0.1f);
    RaycastHit hitInfo;
    //grounded = Physics.CheckSphere(pos, radius, 1 << 8);
    if(Physics.SphereCast(myTransform.position, radius, -Vector3.up, out hitInfo, capsule.height/2f-radius+0.1f, 1<<8, QueryTriggerInteraction.Ignore))
    {
      groundNormal = hitInfo.normal;
      grounded = Vector3.Angle(Vector3.up, groundNormal) <= 45f;
    }
    else
    {
      grounded = false;
    }
  }

  //DEBUG
  IEnumerator PrintState()
  {
    yield return new WaitForSeconds(1f);
    Debug.Log(moveSM.currentState.GetType());
    StartCoroutine(PrintState());
  }
}
