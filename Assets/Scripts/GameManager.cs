using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button placeButton = null;
    [SerializeField] Button infoButton = null;

    private PlantPlacer plantPlacer = null;

    private void Start()
    {
        plantPlacer = FindObjectOfType<PlantPlacer>();
        infoButton.interactable = false;
    }

    private void Update()
    {
        
    }

    public void PlaceButtonClick()
    {
        placeButton.interactable = false;
        infoButton.interactable = true;
        plantPlacer.canPlaceOrRemove = true;
    }

    public void InfoButtonClick()
    {
        infoButton.interactable = false;
        placeButton.interactable = true;
        plantPlacer.canPlaceOrRemove = false;
    }
}
