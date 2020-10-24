using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ONLY TO BE USED FOR REFERENCE

public class PlayerMovement : MonoBehaviour
{
  PlayerInputHandler inputHandler;
  Rigidbody rb;
  CapsuleCollider capsule;
  
  public Transform body;

  public float speed = 10f;
  public float airSpeed = 2f;
  public float wallrunSpeed = 10f;
  public float maxSpeed = 10f;
  public float maxWallSpeed = 10f;
  public float jumpStrength = 10f;
  public float wallJumpStrength = 10f;
  public float movementDeadzone = 0.1f;
  float sqrMaxSpeed;

  public bool running = false;
  public bool grounded = false;
  public bool wallRunning = false;
  public float wallRayDist = 1f;
  bool rightWallHit = false;
  bool leftWallHit = false;
  RaycastHit wallHit;
  public float maxCamTilt = 15f;
  public float tiltMod = 5f;

  void Awake() {
    inputHandler = GetComponent<PlayerInputHandler>();
    rb = GetComponent<Rigidbody>();
    capsule = GetComponent<CapsuleCollider>();
  }

  void Start()
  {
    sqrMaxSpeed = Mathf.Pow(maxSpeed, 2);
  }

  public void HandleMovement(float _dt)
  {
    Vector3 movement = new Vector3(inputHandler.moveInput.x, 0f, inputHandler.moveInput.y) * (grounded ? speed : airSpeed);

    Vector3 localVelocity = LocalVelocity();
    if (Vector3.Project(localVelocity, movement).sqrMagnitude > sqrMaxSpeed) movement = Vector3.zero;
    else rb.AddRelativeForce(movement);

    // If velocity is small enough and player isn't trying to move, simply set it to zero.
    // Otherwise, provide counter movement if the player is on the ground.
    if (rb.velocity.sqrMagnitude > 0f
      && movement == Vector3.zero
      && Mathf.Abs(localVelocity.x) < movementDeadzone
      && Mathf.Abs(localVelocity.z) < movementDeadzone)
    {
      rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
      running = false;
    }
    else if (grounded)
    {
      Vector3 moveDir = movement.normalized;
      Vector3 velDir = localVelocity;
      velDir.y = 0f;
      velDir.Normalize();
      rb.AddRelativeForce((moveDir-velDir)*speed);
      running = true;
    }
  }

  Vector3 LocalVelocity()
  {
    return transform.InverseTransformDirection(rb.velocity);
  }


  public void HandleGround()
  {
    float radius = capsule.radius * 0.9f;
    Vector3 pos = transform.position - Vector3.up*(capsule.height/2f-radius+0.1f);
    grounded = Physics.CheckSphere(pos, radius, 1 << 8);
  }

  public void HandleJump(float _dt)
  {
    if(inputHandler.jumpInput)
    {
      if (grounded)
      {
        rb.AddForce(transform.up*jumpStrength, ForceMode.Impulse);
      }
      else if (wallRunning)
      {
        Vector3 offset = leftWallHit ? wallHit.normal : -wallHit.normal;
        rb.AddForce((transform.up + wallHit.normal*1.5f).normalized*wallJumpStrength, ForceMode.Impulse);
      }
      inputHandler.jumpInput = false;
    }
  }

  public void CheckWalls()
  {
    if (!grounded)
    {
      RaycastHit rightHit, leftHit;
      rightWallHit = Physics.Raycast(transform.position, transform.right, out rightHit, wallRayDist, 1 << 10);
      leftWallHit  = Physics.Raycast(transform.position, -transform.right, out leftHit, wallRayDist, 1 << 10);
      if (rightWallHit && leftWallHit)
      {
        rightWallHit = rightHit.distance < leftHit.distance;
        leftWallHit = leftHit.distance < rightHit.distance;
      }
      if (leftWallHit) wallHit = leftHit;
      else if (rightWallHit) wallHit = rightHit;
    }
    else
    {
      rightWallHit = false;
      leftWallHit = false;
    }
  }

  public void HandleWallrun(float dt)
  {
    if(((inputHandler.moveInput.y > 0 || inputHandler.moveInput.x < 0 || wallRunning) && leftWallHit)
      || ((inputHandler.moveInput.y > 0 || inputHandler.moveInput.x > 0 || wallRunning) && rightWallHit))
    {
      wallRunning = true;
      rb.AddForce(Vector3.up*Physics.gravity.magnitude*0.7f);
      //Debug.Log(wallHit.normal);
      if (rb.velocity.sqrMagnitude > maxWallSpeed*maxWallSpeed)
      {
        float wrSpeed = leftWallHit ? wallrunSpeed : -wallrunSpeed;
        wrSpeed *= (inputHandler.moveInput.y > 0) ? 1 : 0;
        rb.AddForce(Vector3.Cross(wallHit.normal, wallHit.transform.up).normalized*wrSpeed);
      }
      //stick to wall
      rb.AddForce(-wallHit.normal * wallrunSpeed/5f);
    }
    else wallRunning = false;
    CameraTilt(dt);
  }

  void CameraTilt(float dt)
  {
    if (wallRunning)
    {
      body.localRotation = Quaternion.Slerp(body.localRotation, Quaternion.Euler(new Vector3(0,0,leftWallHit ? -maxCamTilt : maxCamTilt)), tiltMod * dt);
    }
    else
    {
      body.localRotation = Quaternion.Slerp(body.localRotation, Quaternion.identity, tiltMod * dt);
    }
  }
}
