using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button button;
    private InfoManager infoManager;

    void Start()
    {
        infoManager = FindObjectOfType<InfoManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(infoManager.OnPlantBackButtonClick);
    }
}
