using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlots : MonoBehaviour
{
    private Image image;
    private Button button;
    private SceneManagement sceneManagement;
    private TMP_Text timePlayedText;

    void Start()
    {
        int buttonIndex = transform.GetSiblingIndex() + 1;
        sceneManagement = SceneManagement.instance;
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        timePlayedText = GetComponentInChildren<TMP_Text>();
        string savePath = Application.persistentDataPath + buttonIndex + ".bin";

        if (File.Exists(savePath))
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

        string screenshotPath = Application.persistentDataPath + buttonIndex + ".png";

        if (File.Exists(screenshotPath))
        {
            byte[] picture = File.ReadAllBytes(screenshotPath);
            Texture2D texture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
            texture.LoadImage(picture);
            image.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0f, 0f));
        }

        button.onClick.AddListener(delegate { sceneManagement.LoadGameScene(); SceneManagement.index = buttonIndex; });
    }
}
