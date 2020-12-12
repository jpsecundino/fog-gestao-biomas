using UnityEngine;
using UnityEngine.UI;

public class Configurations : MonoBehaviour
{
    [SerializeField] Slider musicSlider = null;
    [SerializeField] Slider SFXSlider = null;

    SoundManager soundManager;

    void Start()
    {
        soundManager = SoundManager.instance;
        musicSlider.value = PlayerPrefsController.GetMusicVolume();
        SFXSlider.value = PlayerPrefsController.GetSFXVolume();
    }

    void Update()
    {
        soundManager.ChangeMusicVolume(musicSlider.value);
        soundManager.ChangeSFXVolume(SFXSlider.value);
    }

    public void SaveOnExit()
    {
        PlayerPrefsController.SetMusicVolume(musicSlider.value);
        Debug.Log("Music volume saved and set to " + PlayerPrefsController.GetMusicVolume());

        PlayerPrefsController.SetSFXVolume(SFXSlider.value);
        Debug.Log("SFX volume saved and set to " + PlayerPrefsController.GetSFXVolume());
    }
}
