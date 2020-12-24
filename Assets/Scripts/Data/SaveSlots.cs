using UnityEngine;
using UnityEngine.UI;

public class SaveSlots : MonoBehaviour
{
    private Button button;
    private SceneManagement sceneManagement;

    void Start()
    {
        int buttonIndex = transform.GetSiblingIndex() + 1;
        sceneManagement = SceneManagement.instance;
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { sceneManagement.LoadGameScene(); SceneManagement.index = buttonIndex; });
    }
}
