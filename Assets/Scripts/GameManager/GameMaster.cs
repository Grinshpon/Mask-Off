using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour
{
  public GameObject pauseMenu;
  public TextMeshProUGUI fps;
  GameInputHandler inputHandler;
  public bool paused;

  void Awake()
  {
    inputHandler = GetComponent<GameInputHandler>();
  }

  void Start()
  {
    paused = false;
    Cursor.visible = false;
    pauseMenu.SetActive(false);
    StartCoroutine(ShowFPS());
  }

  void Update()
  {
    if (paused != inputHandler.paused)
    {
      if (inputHandler.paused)
      {
        Pause();
      }
      else
      {
        Resume();
      }
    }
  }

  public void Pause()
  {
    paused = true;
    inputHandler.paused = true;
    Time.timeScale = 0f;
    pauseMenu.SetActive(true);
    Cursor.visible = true;
    AudioListener.pause = true;
  }

  public void Resume()
  {
    paused = false;
    inputHandler.paused = false;
    Time.timeScale = 1f;
    pauseMenu.SetActive(false);
    Cursor.visible = false;
    AudioListener.pause = false;
  }

  public void Quit()
  {
    Application.Quit();
  }

  IEnumerator ShowFPS()
  {
    float timer = 0f;
    while (timer < 1.0f)
    {
      timer += Time.unscaledDeltaTime;
      yield return null;
    }
    fps.text = "FPS: " + Mathf.RoundToInt(1f / Time.unscaledDeltaTime);
    yield return StartCoroutine(ShowFPS());
  }
}
