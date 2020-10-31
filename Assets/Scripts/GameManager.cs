using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // This script is only used in scene without Cinemachine

    [SerializeField] CameraMovement cameraMovement = null;
    //[SerializeField] Transform manualMovementTransform = null;
    [SerializeField] Text debugText = null;

    private Vector3 offset = new Vector3(0f, 5f, -10f);
    private Vector3 cameraFollowPosition;
    private bool edgeScrolling = false;
    private bool cameraCanMove = false;
    private float cameraZoom;

    private void Start()
    {
        cameraMovement.Setup(() => cameraFollowPosition + offset, () => cameraZoom);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            edgeScrolling = !edgeScrolling;
            Debug.Log("Edge Scrolling:" + edgeScrolling);
            debugText.text = "Debug(Pressione ESPAÇO ou o botão direito do mouse)\nMovimento de câmera = " + cameraCanMove + "\nMovimento de canto = " + edgeScrolling;
        }
        if (Input.GetMouseButtonDown(1))
        {
            cameraCanMove = !cameraCanMove;
            Debug.Log("Camera movement = " + cameraCanMove);
            debugText.text = "Debug(Pressione ESPAÇO ou o botão direito do mouse)\nMovimento de câmera = " + cameraCanMove + "\nMovimento de canto = " + edgeScrolling;
        }
        if (Input.GetMouseButtonUp(1))
        {
            cameraCanMove = !cameraCanMove;
            Debug.Log("Camera movement = " + cameraCanMove);
            debugText.text = "Debug(Pressione ESPAÇO ou o botão direito do mouse)\nMovimento de câmera = " + cameraCanMove + "\nMovimento de canto = " + edgeScrolling;
        }
        if (cameraCanMove)
        {
            KeyboardMovement();
        }
        if (edgeScrolling)
        {
            EdgeMovement();
        }

        CameraZoom();
    }

    private void EdgeMovement()
    {
        float moveAmount = 50f;
        float edgeSize = 35f;

        //Right
        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
        }
        //Left
        if (Input.mousePosition.x < edgeSize)
        {
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
        }
        //Up
        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            cameraFollowPosition.z += moveAmount * Time.deltaTime;
        }
        //Down
        if (Input.mousePosition.y < edgeSize)
        {
            cameraFollowPosition.z -= moveAmount * Time.deltaTime;
        }
    }
    private void KeyboardMovement()
    {
        float moveAmount = 50f;

        //Right
        if (Input.GetKey(KeyCode.D))
        {
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
        }
        //Left
        if (Input.GetKey(KeyCode.A))
        {
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
        }
        //Up
        if (Input.GetKey(KeyCode.W))
        {
            cameraFollowPosition.z += moveAmount * Time.deltaTime;
        }
        //Down
        if (Input.GetKey(KeyCode.S))
        {
            cameraFollowPosition.z -= moveAmount * Time.deltaTime;
        }
    }

    private void CameraZoom()
    {
        float zoomChangeAmount = 10f;

        if (Input.mouseScrollDelta.y > 0)
        {
            cameraZoom -= zoomChangeAmount * Time.deltaTime;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            cameraZoom += zoomChangeAmount * Time.deltaTime;
        }

        cameraZoom = Mathf.Clamp(cameraZoom, 40f, 100f);
    }
}
