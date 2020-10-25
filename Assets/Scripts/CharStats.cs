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
}
