using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
  public IState previousState;
  public IState currentState;

  public StateMachine()
  {
    currentState = new BlankState();
  }
  public StateMachine(IState initialState)
  {
    currentState = initialState;
    currentState.Enter();
  }

  public virtual void ChangeState(IState newState)
  {
    if (newState != currentState)
    {
      currentState.Exit();
      previousState = currentState;
      currentState = newState;
      currentState.Enter();
      //Debug.Log(currentState.GetType());
    }
  }

  public void Tick()
  {
    currentState.Tick();
  }

  public void FixedTick()
  {
    currentState.FixedTick();
  }

  public void LateTick()
  {
    currentState.LateTick();
  }
}
