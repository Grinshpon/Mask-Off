using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
  PlayerActions inputActions;

  public Vector2 moveInput; // in 3d x is moveInput.x and z is moveInput.y
  public Vector2 lookInput;
  public bool jumpInput = false;
  public bool crouchInput = false;
  public bool attackInput = false;
  public float leanInput;

  void Start()
  {
    jumpInput = false;
    crouchInput = false;
    attackInput = false;
    Enable();
  }

  public void Enable()
  {
    if (inputActions == null) {
      inputActions = new PlayerActions();
      inputActions.PlayerControl.Movement.performed += (action) => moveInput = action.ReadValue<Vector2>();
      inputActions.PlayerControl.Look.performed += (action) => lookInput = action.ReadValue<Vector2>();
      inputActions.PlayerControl.Jump.started += (action) => jumpInput = true;
      inputActions.PlayerControl.Jump.canceled += (action) => jumpInput = false;
      inputActions.PlayerControl.Crouch.performed += (action) => crouchInput = !crouchInput;
      //inputActions.PlayerControl.Crouch.canceled += (action) => crouchInput = false;
      inputActions.PlayerControl.Lean.performed += (action) => leanInput = action.ReadValue<float>();
      inputActions.PlayerControl.Attack.started += (action) => attackInput = true;
      inputActions.PlayerControl.Attack.canceled += (action) => attackInput = false;
    }
    inputActions.Enable();
  }

  public void Disable()
  {
    if (inputActions != null)
    {
      inputActions.Disable();
    }
  }
}
