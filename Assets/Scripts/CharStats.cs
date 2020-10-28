using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharStats", menuName = "CharStats")]
public class CharStats : ScriptableObject
{
  public float accel;
  public float maxMoveSpeed;
  public float jumpMod;
  public float moveMod;

  public float health;
  public float maxHealth;

  public float SetHealth(float value)
  {
    health = Mathf.Clamp(value, 0f, maxHealth);
    return health;
  }

  public float DecHealth(float amount)
  {
    return SetHealth(health-amount);
  }
  public float IncHealth(float amount)
  {
    return SetHealth(health+amount);
  }
}
