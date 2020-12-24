using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private float musicVolume = 1f;
    private float SFXVolume = 1f;

    private AudioClip sfx, musica;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sfx = Resources.Load<AudioClip>("");
        musica = Resources.Load<AudioClip>("");
        musicVolume = PlayerPrefsController.GetMusicVolume();
        SFXVolume = PlayerPrefsController.GetSFXVolume();
        audioSource.volume = musicVolume;
    }

    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "":
                audioSource.PlayOneShot(sfx, SFXVolume);
                break;
            default:
                break;
        }
    }

    public void ChangeMusic(string music)
    {
        switch (music)
        {
            case "":
                audioSource.clip = musica;
                audioSource.Play();
                break;
            default:
                break;
        }
    }

    public void ChangeSFXVolume(float value)
    {
        SFXVolume = value;
        PlayerPrefsController.SetSFXVolume(value);
    }

    public void ChangeMusicVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefsController.SetMusicVolume(value);
    }
}
