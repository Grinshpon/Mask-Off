using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Resolutions : MonoBehaviour
{
  Resolution[] resolutions;
  public Dropdown resolutionMenu;

  void Start()
  {
    resolutions = Screen.resolutions;
    //resolutionMenu.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[resolutionMenu.value].width, resolutions[resolutionMenu.value].height, false); });
    for (int i = 0; i < resolutions.Length; i++)
    {
      string text = ResToString(resolutions[i]);
      resolutionMenu.options.Add(new Dropdown.OptionData(text));
      if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) resolutionMenu.value = i;
    }
    resolutionMenu.RefreshShownValue();
  }

  string ResToString(Resolution res)
  {
    return res.width + " x " + res.height;
  }
}
