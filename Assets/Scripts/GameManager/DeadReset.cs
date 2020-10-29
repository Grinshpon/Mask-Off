using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadReset : MonoBehaviour
{
  GameInputHandler inputHandler;
  public PlayerController player;
  public GameObject deadScreen;
  bool reloading;

  void Start()
  {
    reloading = false;
    inputHandler = GetComponent<GameInputHandler>();
    deadScreen.SetActive(false);
  }

  void Update()
  {
    if (!player.alive)
    {
      deadScreen.SetActive(true);
      if (inputHandler.resetInput && !reloading)
      {
        reloading = true;
        SceneManager.LoadScene("Game");
      }
    }
  }
}
