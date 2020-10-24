using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Resolutions : MonoBehaviour
{
  Resolution[] resolutions;
  public Dropdown dropdownMenu;

  void Start()
  {
    resolutions = Screen.resolutions;
    dropdownMenu.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height, false); });
    for (int i = 0; i < resolutions.Length; i++)
    {
      string text = ResToString(resolutions[i]);
      dropdownMenu.options.Add(new Dropdown.OptionData(text));
      dropdownMenu.value = i;
    }
  }

  string ResToString(Resolution res)
  {
    return res.width + " x " + res.height;
  }
}
