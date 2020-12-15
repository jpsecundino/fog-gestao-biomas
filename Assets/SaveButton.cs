using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { gameManager.SaveGame(SceneManagement.index); });
    }
}
