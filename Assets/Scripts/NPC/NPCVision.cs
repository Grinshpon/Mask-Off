using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVision : MonoBehaviour
{
  //angle is half of conal fov
  public float angle = 30;

  NPCAgent guard;

  public Vector3 lastKnownPosition;

  public bool seePlayer = false;
  Transform myTransform;
  Transform playerTransform;
  Visibility playerVisibility;

  void Awake()
  {
    guard = GetComponentInParent<NPCAgent>();
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

      float visAngle = Vector3.Angle(myTransform.forward, playerLocalPos);
      seePlayer = false;
      if (visAngle <= angle)
      {
        //Debug.Log("Angle: " + visAngle);
        bool rayhit = Physics.Raycast(
          myPosition,
          playerLocalPos,
          out hitInfo,
          100f,
          Physics.DefaultRaycastLayers & (~(1 << 11)),
          QueryTriggerInteraction.Ignore
        );
        Debug.DrawRay(myPosition, playerLocalPos, Color.red);
        if (rayhit)
        {
          seePlayer = hitInfo.transform.CompareTag("Player");
          //Debug.Log(hitInfo.transform.tag);
          if (seePlayer)
          {
            lastKnownPosition = playerPosition;
            float sqrDist = playerLocalPos.sqrMagnitude;
            float addSuspicion;
            // 2 units
            if (sqrDist < 4f) addSuspicion = playerVisibility.visibility * 10f * Time.deltaTime;
            // 5 units
            else if (sqrDist < 25f) addSuspicion = playerVisibility.visibility * Time.deltaTime;
            else addSuspicion = (playerVisibility.visibility / Mathf.Sqrt(sqrDist)) * 2f * Time.deltaTime;
            guard.suspicionLevel = Mathf.Clamp(guard.suspicionLevel + addSuspicion,0f,100f);
            //Debug.Log("Sus: " + (addSuspicion / Time.deltaTime) + "\n Dist: " + playerLocalPos.magnitude);
          }
        }
      }
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      playerTransform = other.transform;
      playerVisibility = other.gameObject.GetComponent<Visibility>();
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      playerTransform = null;
      playerVisibility = null;
    }
  }
}
