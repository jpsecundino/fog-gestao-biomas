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

    public void LoadButton()
    {
        menuCanvas.SetActive(false);
        loadCanvas.SetActive(true);
    }

    public void BackFromLoadButton()
    {
        loadCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
}
