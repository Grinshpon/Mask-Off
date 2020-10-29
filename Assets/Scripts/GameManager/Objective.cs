using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Objective : MonoBehaviour
{
  //current objective is to kill all guards
  public TextMeshProUGUI tasks;
  public GameObject win;
  int count;
  NPCAgent[] npcs;
  public bool completed;
  bool resetting;

  public void Start()
  {
    win.SetActive(false);
    completed = false;
    resetting = false;
    npcs = FindObjectsOfType<NPCAgent>();
    count = npcs.Length;
  }

  public void Update()
  {
    int killed = 0;
    foreach(NPCAgent guard in npcs)
    {
      if (!guard.alive)
      {
        killed++;
      }
    }
    completed = killed == count;
    tasks.text = "" + killed + "/" + count + " Guards Eliminated";
    if (completed && !resetting)
    {
      resetting = true;
      win.SetActive(true);
      StartCoroutine(ResetGame());
    }
  }

  IEnumerator ResetGame()
  {
    yield return new WaitForSeconds(6f);
    SceneManager.LoadScene("Start");
    yield return null;
  }
}
