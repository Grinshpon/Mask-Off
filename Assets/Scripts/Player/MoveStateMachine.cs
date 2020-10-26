using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStateMachine : StateMachine
{
  public AudioSource footsteps;

  public MoveStateMachine() : base() {}
  public MoveStateMachine(IState initState) : base(initState) {}

  public override void ChangeState(IState newState)
  {
    if (newState != currentState)
    {
      currentState.Exit();
      currentState = newState;
      currentState.Enter();

      switch (currentState)
      {
        case RunningState _:
          footsteps.Play();
          break;
        default:
          footsteps.Pause();
          break;
      }
    }
  }
}
