using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  PlayerInputHandler inputHandler;
  CapsuleCollider capsule;
  PlayerCamera cam;
  Visibility visibility;

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

  public CharStats stats;

  [Header("Flags")]
  public Vector2 movement;
  public bool grounded;
  public bool jumping;
  public bool crouching;
  public float leanDir;

  void Awake()
  {
    stats = Instantiate(stats);
    visibility = GetComponent<Visibility>();

    inputHandler = GetComponent<PlayerInputHandler>();
    capsule = GetComponent<CapsuleCollider>();
    cam = GetComponent<PlayerCamera>();

    runningState = GetComponent<RunningState>();
    idleState = GetComponent<IdleState>();
    midairState = GetComponent<MidairState>();
    moveSM = new StateMachine();

    leanState = GetComponent<LeanState>();
    crouchState = GetComponent<CrouchState>();
  }

  void Start()
  {
    //StartCoroutine(PrintState());
    moveSM.ChangeState(idleState);
  }

  void Update()
  {
    movement = inputHandler.moveInput;
    jumping = jumping || (grounded && inputHandler.jumpInput);
    if (jumping)
    {
      //Debug.Log("Wanna jump");
      inputHandler.jumpInput = false;
    }

    leanDir = inputHandler.leanInput;
    crouching = inputHandler.crouchInput;

    if (grounded) {
      if (movement.sqrMagnitude > 0) moveSM.ChangeState(runningState);
      else moveSM.ChangeState(idleState);
    }
    else moveSM.ChangeState(midairState);

    moveSM.Tick();
    leanState.Tick();
    crouchState.Tick();
    visibility.Tick();
  }

  void FixedUpdate()
  {
    HandleGround();
    moveSM.FixedTick();
  }

  void LateUpdate()
  {
    moveSM.LateTick();
    cam.HandleRotation(Time.deltaTime);
    cam.HandleBobbing(Time.deltaTime);
  }

  public void HandleGround()
  {
    float radius = capsule.radius * 0.9f;
    Vector3 pos = transform.position - Vector3.up*(capsule.height/2f-radius+0.1f);
    grounded = Physics.CheckSphere(pos, radius, 1 << 8);
  }

  //DEBUG
  IEnumerator PrintState()
  {
    yield return new WaitForSeconds(1f);
    Debug.Log(moveSM.currentState.GetType());
    StartCoroutine(PrintState());
  }
}
