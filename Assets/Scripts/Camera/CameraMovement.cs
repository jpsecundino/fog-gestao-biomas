using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float edgeSize = 35f;

    private bool edgeScrolling = false;
    private bool cameraCanMove = false;

    void Update()
    {
        KeyActions();

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0f, 100f), 15f, Mathf.Clamp(transform.position.z, 0f, 100f));

        if (cameraCanMove)
        {
            Movement();
        }
        if (edgeScrolling)
        {
            EdgeMovement();
        }
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
            //cameraFollowPosition.z += moveAmount * Time.deltaTime;
            transform.Translate(0f, 0f, moveSpeed * Time.deltaTime);
        }
        //Down
        if (Input.mousePosition.y < edgeSize)
        {
            //cameraFollowPosition.z -= moveAmount * Time.deltaTime;
            transform.Translate(0f, 0f, -moveSpeed * Time.deltaTime);
        }
    }
    private void Movement()
    {
        transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
    }
}
