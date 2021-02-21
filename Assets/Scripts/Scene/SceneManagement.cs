using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    #region Singleton

    public static SceneManagement instance;

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

    public static int index;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.instance;
    }

    public void LoadMenu()
    {
        soundManager.PlaySound("Button Click");
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        soundManager.PlaySound("Button Click");
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        soundManager.PlaySound("Button Click");
        Application.Quit();
    }
}
