using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Visibility : MonoBehaviour
{
  Lightable lightable;
  PlayerController player;

  public TextMeshProUGUI visText;

  public int visibility;

  void Awake()
  {
    lightable = GetComponent<Lightable>();
    player = GetComponent<PlayerController>();
  }

  public void Tick()
  {
    visibility = Mathf.RoundToInt(100f * Mathf.Clamp(lightable.lightLevel * (player.crouching ? 0.75f : 1f),0f,1f));
    visText.text = "Visibility: " + visibility;
  }
}
