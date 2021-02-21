using UnityEngine;

public class MenuManager : MonoBehaviour
{
    #region Singleton

    public static MenuManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private GameObject menuCanvas = null;
    [SerializeField] private GameObject loadCanvas = null;
    [SerializeField] private GameObject optionsCanvas = null;

    private SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.instance;
    }
    public void LoadButton()
    {
        soundManager.PlaySound("Button Click");
        menuCanvas.SetActive(false);
        loadCanvas.SetActive(true);
    }

    public void BackFromLoadButton()
    {
        soundManager.PlaySound("Button Click");
        loadCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void OptionsButton()
    {
        soundManager.PlaySound("Button Click");
        menuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void BackFromOptionsButton()
    {
        soundManager.PlaySound("Button Click");
        optionsCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
}
