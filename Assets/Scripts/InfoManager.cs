using UnityEngine;
using Cinemachine;

public class InfoManager : MonoBehaviour
{
    public bool canShowInfo { get; set; } = false;

    [Header("Info")]
    [SerializeField] private GameObject soilInfo = null;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera cmVirtualCam = null;
    [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -2f);
    [SerializeField] private GameObject cube = null;

    private GameObject plantHighlighted = null;
    private Vector3 initialOffset = new Vector3(0f, 14.29f, -18.78f);
    private Vector3 velocity = Vector3.zero;
    private Vector3 plantPos = default;
    private Quaternion initialRotation = default;
    private CinemachineTransposer transposer = null;

    private enum CameraStates 
    { 
        ZoomIn,
        ZoomOut,
    }

    [SerializeField] private CameraStates activeCameraState;

    void Start()
    {
        initialRotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
        activeCameraState = CameraStates.ZoomOut;
        transposer = cmVirtualCam.GetCinemachineComponent<CinemachineTransposer>();
    }


    void Update()
    {
        if (canShowInfo)
        {
            OnLeftMouseClick();
        }
        switch (activeCameraState)
        {
            case CameraStates.ZoomIn:
                transposer.m_FollowOffset = Vector3.SmoothDamp(transposer.m_FollowOffset, offset, ref velocity, 2f);
                break;

            case CameraStates.ZoomOut:
                transposer.m_FollowOffset = Vector3.SmoothDamp(transposer.m_FollowOffset, initialOffset, ref velocity, 2f);
                break;

            default:
                break;
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
                //plantInfo.transform.position = hitInfo.transform.position + offset;
                plantPos = hitInfo.transform.position;

                if (hitInfo.transform.CompareTag(StringsReferences.plantTag))
                {
                    plantHighlighted = hitInfo.transform.gameObject;
                    print(plantHighlighted);
                    activeCameraState = CameraStates.ZoomIn;
                    canShowInfo = false;
                    plantHighlighted.GetComponentInChildren<Canvas>().enabled = true;
                    plantHighlighted = hitInfo.transform.gameObject;
                    plantHighlighted.GetComponent<Outline>().enabled = true;
                    cmVirtualCam.Follow = hitInfo.transform;
                }
                /*
                if (hitInfo.transform.CompareTag(StringsReferences.groundTag))
                {
                    soilInfo.SetActive(true);
                    cmVirtualCam.Follow = hitInfo.transform;
                    cmVirtualCam.LookAt = hitInfo.transform;
                    activeCameraState = CameraStates.ZoomIn;
                }*/
            }
        }
    }

    public void OnPlantBackButtonClick()
    {
        canShowInfo = true;
        activeCameraState = CameraStates.ZoomOut;
        plantHighlighted.GetComponentInChildren<Canvas>().enabled = false;
        cube.transform.position = plantPos;
        cmVirtualCam.Follow = cube.transform;
        plantHighlighted.GetComponent<Outline>().enabled = false;
    }

    public void OnSoilBackButtonClick()
    {
        canShowInfo = true;
        activeCameraState = CameraStates.ZoomOut;
        soilInfo.SetActive(false);
        cube.transform.position = plantPos;
        cmVirtualCam.Follow = cube.transform;
    }
}
