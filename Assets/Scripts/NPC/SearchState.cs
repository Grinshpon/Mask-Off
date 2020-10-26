using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Suspicious State
public class SearchState : State
{
  public override void Enter()
  {
    Debug.Log("What was that?");
  }
}
