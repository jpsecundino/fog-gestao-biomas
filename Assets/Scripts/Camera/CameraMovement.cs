using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float edgeSize = 35f;
    [SerializeField] float zoomVelocity = 1.5f;
    [SerializeField] CinemachineVirtualCamera farCamera = null;

    private CinemachineTransposer farCameraTransposer;
    private bool edgeScrolling = false;
    private bool cameraCanMove = false;

    private void Start()
    {
         farCameraTransposer = farCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    void Update()
    {
        KeyActions();
        Zoom();

        if (cameraCanMove)
        {
            Movement();
        }
        if (edgeScrolling)
        {
            EdgeMovement();
        }

        // Limitando a posiçao da camera
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0f, 100f), 15f, Mathf.Clamp(transform.position.z, 0f, 100f));
    }

    private void KeyActions()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            edgeScrolling = !edgeScrolling;
        }
        if (Input.GetMouseButtonDown(1))
        {
            cameraCanMove = !cameraCanMove;
        }
        if (Input.GetMouseButtonUp(1))
        {
            cameraCanMove = !cameraCanMove;
        }
    }

    private void EdgeMovement()
    {
        //Right
        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);
        }
        //Left
        if (Input.mousePosition.x < edgeSize)
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0f, 0f);
        }
        //Up
        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            transform.Translate(0f, 0f, moveSpeed * Time.deltaTime);
        }
        //Down
        if (Input.mousePosition.y < edgeSize)
        {
            transform.Translate(0f, 0f, -moveSpeed * Time.deltaTime);
        }
    }
    private void Movement()
    {
        transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
    }

    private void Zoom()
    {
        farCameraTransposer.m_FollowOffset.y -= Input.mouseScrollDelta.y * zoomVelocity;
        farCameraTransposer.m_FollowOffset.z += Input.mouseScrollDelta.y * zoomVelocity;
        farCameraTransposer.m_FollowOffset.y = Mathf.Clamp(farCameraTransposer.m_FollowOffset.y, 5f, 35f);
        farCameraTransposer.m_FollowOffset.z = Mathf.Clamp(farCameraTransposer.m_FollowOffset.z, -25f, 5f);
    }
}
