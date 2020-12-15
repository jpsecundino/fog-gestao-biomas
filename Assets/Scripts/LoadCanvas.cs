using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadCanvas : MonoBehaviour
{
    void Start()
    {/*
        saveData = new SaveData[4];
        gameManager = GameManager.instance;
        sceneManagement = SceneManagement.instance;
        path = Application.persistentDataPath + "/GestaoBiomasSave";

        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(path + i + ".bin"))
                saveData[i] = SaveSystem.LoadGame(i);
        }

        for (int i = 0; i < 4; i++)
        {
            if (buttons[i] != null)
            {
                buttons[i].onClick.AddListener(
                    delegate
                    {
                        gameManager.LoadGame(i);
                        sceneManagement.LoadGameScene();
                    });
            }
            else
            {
                buttons[i].onClick.AddListener(
                    delegate
                    {
                        gameManager.SaveGame(i);
                        sceneManagement.LoadGameScene();
                    });
            }
        }*/
    }
}
