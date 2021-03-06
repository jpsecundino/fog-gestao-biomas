﻿using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InfoManager : MonoBehaviour
{
    #region Singleton

    public static InfoManager instance;

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

    public bool canShowInfo { get; set; } = false;

    [Header("Info")]
    [SerializeField] private GameObject soilInfo = null;

    [Header("Camera")]
    public CinemachineVirtualCamera cmVirtualCamClose = null;
    public GameObject cube = null;
    [SerializeField] private Image image = null;

    private RaycastHit hitInfo;
    private Ray ray;
    private GameObject plantHighlighted = null;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (canShowInfo)
        {
            OnLeftMouseClick();
        }
    }

    private void OnLeftMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.CompareTag(StringsReferences.plantTag))
                {
                    image.enabled = true;
                    plantHighlighted = hitInfo.transform.gameObject;
                    canShowInfo = false;
                    plantHighlighted.GetComponentInChildren<Canvas>().enabled = true;
                    plantHighlighted = hitInfo.transform.gameObject;
                    plantHighlighted.GetComponent<Outline>().enabled = true;
                    cmVirtualCamClose.Follow = hitInfo.transform;
                    cmVirtualCamClose.Priority = 11;
                }
            }
        }
    }

    public void OnPlantBackButtonClick()
    {
        canShowInfo = true;
        plantHighlighted.GetComponentInChildren<Canvas>().enabled = false;
        image.enabled = false;
        cmVirtualCamClose.Priority = 9;
        plantHighlighted.GetComponent<Outline>().enabled = false;
    }

    public void OnSoilBackButtonClick()
    {
        canShowInfo = true;
        soilInfo.SetActive(false);
        cmVirtualCamClose.Priority = 9;
    }
}
