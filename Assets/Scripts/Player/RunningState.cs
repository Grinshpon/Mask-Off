using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : MoveState
{
  AudioSource footsteps;

  public override void FixedTick()
  {
    UpdateVars();
    HandleJump();
    HandleMovement();
    HandleCounterMovement();
  }

  public override void Enter()
  {
    if (footsteps == null)
    {
      footsteps = GetComponent<AudioSource>();
    }
    if (!footsteps.isPlaying) footsteps.Play();
    footsteps.loop = true;
  }

  public override void Exit()
  {
    footsteps.loop = false;
  }
}
