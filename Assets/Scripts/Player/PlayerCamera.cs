using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
  PlayerController player;
  PlayerInputHandler inputHandler;
  Rigidbody rb;
  public Transform camPivotTransform;
  public Transform headTransform;

  public float sensitivity = 1f;

  public float lookAngle = 0f;
  public float pivotAngle = 0f;

  float bobZ = 0f;
  Vector3 origin;
  Vector3 position;

  void Awake()
  {
    inputHandler = GetComponent<PlayerInputHandler>();
    player = GetComponent<PlayerController>();
    rb = GetComponent<Rigidbody>();
  }

  void Start()
  {
    bobZ = 0f;
    origin = headTransform.localPosition;
    position = Vector3.zero;
  }

  public void HandleRotation(float dt)
  {
    lookAngle += inputHandler.lookInput.x * sensitivity * dt;
    pivotAngle += -inputHandler.lookInput.y * sensitivity * dt;
    pivotAngle = Mathf.Clamp(pivotAngle, -90f, 90f);

    Rotate();
  }

  public void Rotate()
  {
    Vector3 rotation = Vector3.zero;
    rotation.y = lookAngle;
    transform.localRotation = Quaternion.Euler(rotation);

    rotation = Vector3.zero;
    rotation.x = pivotAngle;
    camPivotTransform.localRotation = Quaternion.Euler(rotation);
  }

  public void HandleBobbing(float dt)
  {
    switch (player.moveSM.currentState)
    {
      case RunningState _:
        bobZ += rb.velocity.magnitude*dt;
        float bobHeight = 0.08f*Mathf.Sin(bobZ);
        position = new Vector3(0f,bobHeight,0f);
        break;
      default:
        position = Vector3.Lerp(position, Vector3.zero, 10f*dt);
        bobZ = Mathf.Asin(position.y);
        break;
    }
    headTransform.localPosition = origin + position;
  }
}
