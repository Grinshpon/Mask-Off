using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputHandler : MonoBehaviour
{
  GlobalActions inputActions;
  public bool paused;

  void Start()
  {
    paused = false;
    Enable();
  }

  public void Enable()
  {
    if (inputActions == null)
    {
      inputActions = new GlobalActions();
      inputActions.Actions.Pause.performed += (action) => paused = !paused;
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
