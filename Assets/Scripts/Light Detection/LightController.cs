using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
  // Remember: intensity follows the inverse square law,
  //light attenuation formula: intensity (i) at a distance (d) from light with configured intensity (I) and range (R):
  // i = I / (1 + 25*(d/R)^2)
  Light myLight;
  SphereCollider sphereCollider;

  void Start() {
    myLight = GetComponent<Light>();
    sphereCollider = GetComponent<SphereCollider>();
  }

  void Update() {
    sphereCollider.radius = myLight.range;
  }
}
