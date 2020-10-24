using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour, IState
{
  public virtual void Enter() {}
  public virtual void Tick() {}
  public virtual void FixedTick() {}
  public virtual void LateTick() {}
  public virtual void Exit() {}
}
