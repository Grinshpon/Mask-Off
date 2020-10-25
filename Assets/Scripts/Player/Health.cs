using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
  PlayerController player;
  public TextMeshProUGUI hp;

  void Awake()
  {
    player = GetComponent<PlayerController>();
  }

  void Start()
  {
    player.stats.health = player.stats.maxHealth;
  }

  void Update()
  {
    hp.text = "HP: " + player.stats.health;
  }
}
