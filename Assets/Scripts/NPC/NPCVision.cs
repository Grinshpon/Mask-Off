using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVision : MonoBehaviour
{
  public float angle = 30;

  NPCAgent guard;

  public Vector3 lastKnownPosition;
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
      Vector3 myPosition = myTransform.position;
      Vector3 playerPosition = playerTransform.position;
      Vector3 playerLocalPos = playerPosition - myPosition;
      RaycastHit hitInfo;
      if (Vector3.Angle(myTransform.forward, playerLocalPos) <= angle
        && Physics.Raycast(myPosition, playerLocalPos, out hitInfo, 100f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
      {
        if (hitInfo.transform.tag == "Player")
        {
          lastKnownPosition = playerPosition;
          guard.suspicionLevel += Mathf.Clamp(playerVisibility.visibility - playerLocalPos.magnitude,0f,100f) * Time.deltaTime;
          //Debug.Log("I SEE YOU");
        }
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
