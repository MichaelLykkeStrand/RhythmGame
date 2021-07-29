using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static SettingsController instance;
    [SerializeField] Slider slider;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        TransitionController.SetLoading(false);
        instance = this;
        slider.maxValue = 0;
        slider.maxValue = 1;
        slider.value = GetVolume();
        fullscreenToggle.isOn = IsFullscreen();
        slider.onValueChanged.AddListener(delegate { VolumeSliderChanged(); });
        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        exitButton.onClick.AddListener(delegate { Close(); });
    }

    private void VolumeSliderChanged()
    {
        SetVolume(slider.value);
    }

    private void OnFullscreenToggle()
    {
        SetFullscreen(fullscreenToggle.isOn);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        Save();
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("volume");
    }

    public bool IsFullscreen()
    {
        return PlayerPrefs.GetInt("fullscreen") == 1;
    }

    public void SetFullscreen(bool fullscreen)
    {
        int val = fullscreen ? 1 : 0;
        PlayerPrefs.SetInt("fullscreen", val);
        Save();
    }

    public void Reload()
    {

    }


    public void Save()
    {
        PlayerPrefs.Save();
    }
}
