using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMotion : MonoBehaviour
{
  PlayerInputHandler inputHandler;
  public float sensitivity;
  public float maxAmount;
  public float lerpMod;
  public float rotMod;
  public float maxRotAmount;
  public float rotSensitivity;

  private Vector3 initialPosition;
  private Quaternion initialRotation;

  void Start()
  {
    initialPosition = transform.localPosition;
    initialRotation = transform.localRotation;
    inputHandler = GetComponentInParent<PlayerInputHandler>();
  }

  void Update()
  {
    float dt = Time.deltaTime;

    Vector3 finalPosition = Vector3.zero; 
    finalPosition.x = Mathf.Clamp(-inputHandler.lookInput.x*sensitivity*dt, -maxAmount, maxAmount);
    finalPosition.y = Mathf.Clamp(-inputHandler.lookInput.y*sensitivity*dt, -maxAmount, maxAmount);
    transform.localPosition = Vector3.Slerp(transform.localPosition, finalPosition+initialPosition, dt*lerpMod);

    Vector3 finalRotation = Vector3.zero;
    //finalRotation.y = Mathf.Clamp(inputHandler.lookInput.x*rotSensitivity*dt, -maxRotAmount, maxRotAmount);
    finalRotation.z = Mathf.Clamp(-inputHandler.lookInput.x*rotSensitivity*dt, -maxRotAmount, maxRotAmount);
    finalRotation.x = Mathf.Clamp(-inputHandler.lookInput.y*rotSensitivity*dt, -maxRotAmount, maxRotAmount);
    transform.localRotation = Quaternion.Slerp(transform.localRotation, initialRotation*Quaternion.Euler(finalRotation), dt*rotMod);
  }
}
