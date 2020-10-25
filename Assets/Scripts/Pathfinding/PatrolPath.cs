using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
  public List<Buoy> buoys;

  void Awake()
  {
    buoys = new List<Buoy>(GetComponentsInChildren<Buoy>());
    buoys.Sort(Buoy.Compare);
  }

}
