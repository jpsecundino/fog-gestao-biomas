using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private Button button;
    private SceneManagement sceneManagement;

    void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { sceneManagement.Exit(); });
    }
}
