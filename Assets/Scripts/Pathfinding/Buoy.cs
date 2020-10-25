using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoy : MonoBehaviour
{
  [SerializeField]
  public int order;

  public Vector3 position;

  void Awake()
  {
    RaycastHit hitInfo;
    if (Physics.Raycast(transform.position,-Vector3.up, out hitInfo, 10f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
    {
      position = hitInfo.point;
    }
    else
    {
      position = transform.position;
    }
  }

  public static int Compare(Buoy lhs, Buoy rhs)
  {
    if (lhs.order < rhs.order) return -1;
    else if (lhs.order > rhs.order) return 1;
    else return 0;
  }

  void OnDrawGizmosSelected()
  {
    // Draw a yellow sphere at the transform's position
    Gizmos.color = Color.green;
    Gizmos.DrawSphere(transform.position, 0.5f);
  }
}
