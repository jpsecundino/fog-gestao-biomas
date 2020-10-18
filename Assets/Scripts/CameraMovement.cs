using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPositionFunc;
    private Func<float> GetCameraZoomFunc;

    public void Setup(Func<Vector3> GetCameraFollowPositionFunc, Func<float> GetCameraZoomFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
        this.GetCameraZoomFunc = GetCameraZoomFunc;
    }

    private void Update()
    {
        Movement();
        Zoom();
    }

    private void Movement()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        Vector3 cameraMoveDirection = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 2f;

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDirection * distance * cameraMoveSpeed * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                newCameraPosition = cameraFollowPosition;
            }
            transform.position = newCameraPosition;
        }
    }

    private void Zoom()
    {
        /*
        float cameraZoom = GetCameraZoomFunc();
        float distance = Vector3.Distance(cameraZoom, transform.position);
        float cameraMoveSpeed = 2f;

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDirection * distance * cameraMoveSpeed * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                newCameraPosition = cameraFollowPosition;
            }
            transform.position = newCameraPosition;
        }*/
    }
}
