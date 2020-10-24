using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleState1 : State
{
  private float timer;
  Transform placeholder;

  public void Awake()
  {
    timer = 0f;
    placeholder = GetComponent<Transform>();
  }

  public override void Enter()
  {
    Debug.Log("example state (1) entered");
    timer = 0f;
  }

  public override void Tick()
  {
    timer += Time.deltaTime;
    if (timer >= 1f)
    {
      Debug.Log(placeholder.localPosition);
      timer = 0f;
    }
  }

  public override void Exit()
  {
  }
}
