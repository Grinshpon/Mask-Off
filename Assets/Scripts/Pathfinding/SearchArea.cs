using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea
{
  public Vector3 origin;
  public float radius;

  public SearchArea(Vector3 origin, float radius)
  {
    this.origin = origin;
    this.radius = radius;
  }

  public Vector3 RandomPoint()
  {
    //randomize direction then randomize magnitude, then add origin offset
    Vector3 dir;
    dir.x = Random.Range(0f,1f);
    dir.y = Random.Range(0f,1f);
    dir.z = Random.Range(0f,1f);
    dir.Normalize();
    dir *= Random.Range(0f,radius);

    Vector3 position = origin+dir;
    RaycastHit hitInfo;
    if (Physics.Raycast(position, -Vector3.up, out hitInfo, 100f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
    {
      position = hitInfo.point;
    }

    return position;
  }
}
