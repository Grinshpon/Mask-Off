using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
  public AudioMixer audioMixer;
  public Toggle fullscreen;
  public Slider volumeSlider;

  public void Awake()
  {
    fullscreen.isOn = Screen.fullScreen;
    float volume;
    audioMixer.GetFloat("volume", out volume);
    volumeSlider.value = volume;
  }

  public void SetVolume(float volume)
  {
    audioMixer.SetFloat("volume", volume);
  }

  public void SetFullscreen(bool isFullscreen)
  {
    Screen.fullScreen = isFullscreen;
  }

  public void SetResolution(int resolutionIndex)
  {
    Resolution res = Screen.resolutions[resolutionIndex];
    Screen.SetResolution(res.width,res.height,Screen.fullScreen);
  }

  public void SetGraphics(int qualityIndex)
  {
    QualitySettings.SetQualityLevel(qualityIndex);
  }
}
