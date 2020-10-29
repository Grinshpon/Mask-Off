using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Splash : MonoBehaviour
{
  public TextMeshProUGUI blurb;
  GlobalActions inputActions;
  IEnumerator splash;

  void Start()
  {
    blurb.alpha = 0f;
    splash = DoSplash();
    StartCoroutine(splash);
    Enable();
  }

  public void Enable()
  {
    if (inputActions == null)
    {
      inputActions = new GlobalActions();
      inputActions.Actions.Skip.performed += (action) => SkipSplash();
    }
    inputActions.Enable();
  }

  public void Disable()
  {
    if (inputActions != null)
    {
      inputActions.Disable();
    }
  }

  void SkipSplash()
  {
    Disable();
    StopCoroutine(splash);
    StartCoroutine(Cont());
  }

  IEnumerator Cont()
  {
    while (blurb.alpha > 0f)
    {
      blurb.alpha -= Time.deltaTime;
      yield return null;
    }
    SceneManager.LoadScene("Start");
    yield return null;
  }

  IEnumerator DoSplash()
  {
    while (blurb.alpha < 1f)
    {
      blurb.alpha += Time.deltaTime;
      yield return null;
    }
    yield return new WaitForSeconds(20f);
    yield return StartCoroutine(Cont());
  }
}
