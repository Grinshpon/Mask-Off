using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : MonoBehaviour
{
  CapsuleCollider capsule;
  PlayerController player;
  public Transform upperBody;

  public float crouchMod = 0.5f;
  public float crouchSpeed = 5f;
  public float capsuleCrouchHeight = 1.2f;
  public float ubCrouchHeight = -0.25f;

  float desiredCapsuleHeight;
  float desiredUbHeight;

  float maxSpeed;

  void Awake()
  {
    capsule = GetComponent<CapsuleCollider>();
    player = GetComponent<PlayerController>();
  }

  void Start()
  {
    maxSpeed = player.stats.maxMoveSpeed;
  }

  public void Tick()
  {
    float dt = Time.deltaTime;

    if (player.crouching)
    {
      desiredCapsuleHeight = capsuleCrouchHeight;
      desiredUbHeight = ubCrouchHeight;
      player.stats.maxMoveSpeed = maxSpeed * crouchMod;
    }
    else
    {
      desiredCapsuleHeight = 2f;
      desiredUbHeight = 0f;
      player.stats.maxMoveSpeed = maxSpeed;
    }

    capsule.height = Mathf.Lerp(capsule.height, desiredCapsuleHeight, crouchSpeed*dt);
    upperBody.localPosition = Vector3.Lerp(upperBody.localPosition, new Vector3(0f,desiredUbHeight, 0f), crouchSpeed*dt);
  }
}
