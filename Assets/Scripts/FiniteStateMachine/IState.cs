using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
  void Enter();
  void Tick();
  void FixedTick();
  void LateTick();
  //void SFixedUpdate();
  //void SUpdate();
  //void SLateUpdate();
  void Exit();
}
