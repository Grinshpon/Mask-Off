using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVision : MonoBehaviour
{
  public float angle = 30;

  NPCAgent guard;

  Transform myTransform;
  Transform playerTransform;
  Visibility playerVisibility;

  void Awake()
  {
    guard = GetComponent<NPCAgent>();
    myTransform = transform;
  }

  void Update()
  {
    if (playerTransform != null)
    {
      Vector3 playerLocalPos = playerTransform.position - myTransform.position;
      if (Vector3.Angle(myTransform.forward, playerLocalPos) <= angle)
      {
        guard.suspicionLevel += Mathf.Clamp(playerVisibility.visibility - playerLocalPos.magnitude,0f,100f) * Time.deltaTime;
        //Debug.Log("I SEE YOU");
      }
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      playerTransform = other.transform;
      playerVisibility = other.gameObject.GetComponent<Visibility>();
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
    {
      playerTransform = null;
      playerVisibility = null;
    }
  }
}
