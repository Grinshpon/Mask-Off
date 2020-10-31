using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
  protected PlayerController player;
  protected Rigidbody rb;

  protected Vector3 movement;
  protected Vector3 localVelocity;

  [SerializeField]
  protected float moveMod = 1f;

  protected float sqrMaxSpeed;

  void Awake()
  {
    player = GetComponent<PlayerController>();
    rb = GetComponent<Rigidbody>();
    sqrMaxSpeed = Mathf.Pow(player.stats.maxMoveSpeed,2);
  }

  public override void FixedTick()
  {
    UpdateVars();
    HandleMovement();
  }

  protected void UpdateVars()
  {
    movement = new Vector3(player.movement.x, 0f, player.movement.y);
    movement = Vector3.ProjectOnPlane(movement, player.groundNormal);
    localVelocity = LocalVelocity();
    sqrMaxSpeed = Mathf.Pow(player.stats.maxMoveSpeed,2);
  }

  protected void HandleMovement()
  {
    if (Vector3.Project(localVelocity, movement).sqrMagnitude > sqrMaxSpeed) movement = Vector3.zero;
    else rb.AddRelativeForce(movement*player.stats.accel*moveMod);
  }

  protected void HandleCounterMovement()
  {
    Vector3 moveDir = movement;
    Vector3 velDir = localVelocity;
    velDir.y = 0f;
    velDir.Normalize();
    rb.AddRelativeForce((moveDir-velDir)*player.stats.accel*moveMod);
  }

  protected void HandleJump()
  {
    if (player.jumping)
    {
      //Debug.Log("Jump");
      rb.AddRelativeForce(Vector3.up*player.stats.jumpMod, ForceMode.Impulse);
      player.jumping = false;
    }
  }

  protected Vector3 LocalVelocity()
  {
    return transform.InverseTransformDirection(rb.velocity);
  }
}
