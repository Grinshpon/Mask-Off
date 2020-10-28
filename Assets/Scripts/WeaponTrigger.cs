using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
  public bool touchingPlayer;
  
  void Start()
  {
    touchingPlayer = false;
  }

  void OnTriggerEnter(Collider other)
  {
    touchingPlayer = other.CompareTag("Player");
  }

  void OnTriggerExit(Collider other)
  {
    touchingPlayer = other.CompareTag("Player");
  }
}
