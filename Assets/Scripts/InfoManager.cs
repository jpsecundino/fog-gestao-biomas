using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InfoManager : MonoBehaviour
{
    public bool canShowInfo { get; set; } = false;

    [SerializeField] private GameObject plantInfo = null;
    [SerializeField] private GameObject soilInfo = null;
    [SerializeField] private CinemachineVirtualCamera cmVirtualCam = null;
    [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -2f);
    [SerializeField] private GameObject cube = null;

    private GameObject plantHighlighted = null;
    private Vector3 initialOffset = new Vector3(0f, 14.29f, -18.78f); 
    private Vector3 velocity = Vector3.zero;

    private CinemachineTransposer transposer = null;

    private enum CameraStates 
    { 
        ZoomIn, 
        ZoomOut, 
        Default
    }

    [SerializeField] private CameraStates cameraStates;

    void Start()
    {
        cameraStates = CameraStates.Default;
        transposer = cmVirtualCam.GetCinemachineComponent<CinemachineTransposer>();
    }


    void Update()
    {
        if (canShowInfo)
        {
            OnLeftMouseClick();
        }
        switch (cameraStates)
        {
            case CameraStates.ZoomIn:
                transposer.m_FollowOffset = Vector3.SmoothDamp(transposer.m_FollowOffset, offset, ref velocity, 2f);
                break;

            case CameraStates.ZoomOut:
                transposer.m_FollowOffset = Vector3.SmoothDamp(transposer.m_FollowOffset, initialOffset, ref velocity, 2f);
                break;

            case CameraStates.Default:
                break;

            default:
                break;
        }
        if (Vector3.Distance(transposer.m_FollowOffset, initialOffset) < 0.001f)
        {
            cameraStates = CameraStates.Default;
        }
    }

    private void OnLeftMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                plantInfo.transform.position = hitInfo.transform.position + offset;

                if (hitInfo.transform.CompareTag(StringsReferences.plantTag))
                {
                    canShowInfo = false;
                    plantInfo.SetActive(true);
                    plantHighlighted = hitInfo.transform.gameObject;
                    plantHighlighted.GetComponent<Outline>().enabled = true;
                    cmVirtualCam.Follow = hitInfo.transform.gameObject.transform;
                    cmVirtualCam.LookAt = hitInfo.transform.gameObject.transform;
                    cameraStates = CameraStates.ZoomIn;
                }
                if (hitInfo.transform.CompareTag(StringsReferences.groundTag))
                {
                    soilInfo.SetActive(true);
                    cmVirtualCam.Follow = hitInfo.transform;
                    cmVirtualCam.LookAt = hitInfo.transform;
                    cameraStates = CameraStates.ZoomIn;
                }
            }
        }
    }

    public void OnPlantBackButtonClick()
    {
        canShowInfo = true;
        cameraStates = CameraStates.ZoomOut;
        plantInfo.SetActive(false);
        cmVirtualCam.Follow = cube.transform;
        cmVirtualCam.LookAt = cube.transform;
        plantHighlighted.GetComponent<Outline>().enabled = false;
    }
    public void OnSoilBackButtonClick()
    {
        canShowInfo = true;
        cameraStates = CameraStates.ZoomOut;
        soilInfo.SetActive(false);
        cmVirtualCam.Follow = cube.transform;
        cmVirtualCam.LookAt = cube.transform;
    }
}
