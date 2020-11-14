using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button placeButton = null;
    [SerializeField] private Button infoButton = null;

    private PlantPlacer plantPlacer = null;
    private InfoManager infoManager = null;

    private void Start()
    {
        plantPlacer = FindObjectOfType<PlantPlacer>();
        infoManager = FindObjectOfType<InfoManager>();
        placeButton.interactable = false;
    }

    public void PlaceButtonClick()
    {
        placeButton.interactable = false;
        infoButton.interactable = true;
        plantPlacer.canPlaceOrRemove = true;
        infoManager.canShowInfo = false;
    }

    public void InfoButtonClick()
    {
        infoButton.interactable = false;
        placeButton.interactable = true;
        plantPlacer.canPlaceOrRemove = false;
        infoManager.canShowInfo = true;
    }
}
