using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lightable : MonoBehaviour {
  public float lightLevel = 0f; //eventually starting value will be set to "ambient light level"
  public List<Collider> lightSources;
  public List<Light> lights;

  public Scrollbar scrollBar;

  void FixedUpdate() {
    HandleLightLevel();
    if (scrollBar) {
      scrollBar.value = lightLevel/5;
    }
  }

  void OnTriggerEnter(Collider other) {
    switch (other.tag) {
      case "Light":
        HandleLightEntered(other);
        break;
      default: break;
    }
  }

  void OnTriggerExit(Collider other) {
    switch (other.tag) {
      case "Light":
        HandleLightExited(other);
        break;
      default: break;
    }
  }

  void HandleLightEntered(Collider other) {
    lightSources.Add(other);
    lights.Add(other.GetComponent<Light>());
  }

  void HandleLightExited(Collider other) {
    lightSources.Remove(other);
    lights.Remove(other.GetComponent<Light>());
  }

  void HandleLightLevel() {
    lightLevel = 0f;
    RaycastHit hitInfo;
    Ray ray = new Ray();
    Vector3 direction;
    float distance;
    Collider lightSource;
    Light light;
    for(int i = 0; i < lightSources.Count; i++) {
      lightSource = lightSources[i];
      light = lights[i];
      direction = lightSource.transform.position - transform.position;
      ray.origin = transform.position;
      ray.direction = direction;
      distance = direction.magnitude;
      Debug.DrawRay(ray.origin, direction, Color.red);
      if (Physics.Raycast(ray, out hitInfo, distance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)) {
        //if (hitInfo.collider != lightSource) {
        //}
      }
      else {
        //light attenuation formula: intensity (i) at a distance (d) from light with configured intensity (I) and range (R):
        // i = I / (1 + 25*(d/R)^2)
        lightLevel += light.intensity / (1f + 25f*Mathf.Pow(distance/light.range,2));
      }
    }
  }
}
