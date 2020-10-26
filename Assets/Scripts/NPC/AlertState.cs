using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : State
{
  public override void Enter()
  {
    Debug.Log("Never should have come here!");
  }
}
