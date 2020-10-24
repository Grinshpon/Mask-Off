using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanState : MonoBehaviour
{
  public enum Lean
  {
    Up,
    Left,
    Right,
  }

  public Transform upperBody;
  PlayerController player;

  public Lean lean;

  public float maxTilt;
  public float tiltSpeed;

  Vector3 desiredRotation;

  void Awake()
  {
    player = GetComponent<PlayerController>();
  }

  void Start()
  {
    desiredRotation = Vector3.zero;
  }

  public void SetLean(Lean lean)
  {
    this.lean = lean;
  }

  public void Tick()
  {
    switch (player.leanDir)
    {
      case 0:
        lean = Lean.Up;
        break;
      case 1:
        lean = Lean.Right;
        break;
      case -1:
        lean = Lean.Left;
        break;
      default: break;
    }

    switch (lean)
    {
      case Lean.Up:
        desiredRotation.z = 0f;
        break;
      case Lean.Left:
        desiredRotation.z = -maxTilt;
        break;
      case Lean.Right:
        desiredRotation.z = maxTilt;
        break;
    }
    upperBody.localRotation = Quaternion.Slerp(upperBody.localRotation, Quaternion.Euler(desiredRotation), tiltSpeed*Time.deltaTime);
  }
}
