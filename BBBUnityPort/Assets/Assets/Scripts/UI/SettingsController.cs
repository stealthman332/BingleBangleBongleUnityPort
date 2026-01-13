using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    private const string MasterVolKey = "masterVolume";
    private const string FullscreenKey = "fullscreen";
    private const string ResIndexKey = "resolutionIndex";

    private void OnEnable()
    {
        
        // Volume
        float vol = PlayerPrefs.GetFloat(MasterVolKey, 1f);
        masterVolumeSlider.value = vol;
        AudioListener.volume = vol;

        // Fullscreen
        bool fs = PlayerPrefs.GetInt(FullscreenKey, 1) == 1;
        fullscreenToggle.isOn = fs;
        Screen.fullScreen = fs;

        // Resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        var options = new List<string>();
        int currentIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            var r = resolutions[i];
            string option = $"{r.width} x {r.height} @ {r.refreshRateRatio.value:0.#}Hz";
            options.Add(option);

            if (r.width == Screen.currentResolution.width && r.height == Screen.currentResolution.height)
                currentIndex = i;
        }

        resolutionDropdown.AddOptions(options);

        int savedIndex = PlayerPrefs.GetInt(ResIndexKey, currentIndex);
        savedIndex = Mathf.Clamp(savedIndex, 0, resolutions.Length - 1);
        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Hook these to UI events
    public void SetMasterVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(MasterVolKey, value);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(FullscreenKey, isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetResolution(int index)
    {
        index = Mathf.Clamp(index, 0, resolutions.Length - 1);
        var r = resolutions[index];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);

        PlayerPrefs.SetInt(ResIndexKey, index);
        PlayerPrefs.Save();
    }
}
