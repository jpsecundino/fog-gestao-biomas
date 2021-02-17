using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlots : MonoBehaviour
{
    private Button button;
    private SceneManagement sceneManagement;
    private TMP_Text timePlayedText;

    void Start()
    {
        int buttonIndex = transform.GetSiblingIndex() + 1;
        sceneManagement = SceneManagement.instance;
        button = GetComponent<Button>();
        timePlayedText = GetComponentInChildren<TMP_Text>();
        string path = Application.persistentDataPath + "/GestaoBiomasSave" + buttonIndex + ".bin";

        if (File.Exists(path))
        {
            float playingTime = SaveSystem.LoadGame(buttonIndex).playingTime;
            string hours = Mathf.Floor((playingTime % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((playingTime % 3600) / 60).ToString("00");
            string seconds = Mathf.Floor(playingTime % 60).ToString("00");
            timePlayedText.text = "Tempo de jogo: " + hours + ":" + minutes + ":" + seconds;
        }
        else
        {
            timePlayedText.text = "Novo jogo";
        }

        button.onClick.AddListener(delegate { sceneManagement.LoadGameScene(); SceneManagement.index = buttonIndex; });
    }
}
