using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float edgeSize = 35f;
    [SerializeField] Text debugText = null;

    private bool edgeScrolling = false;
    private bool cameraCanMove = false;

    void Update()
    {
        KeyActions();

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
            debugText.text = "Debug(Pressione ESPAÇO ou o botão direito do mouse)\nMovimento de câmera = " + cameraCanMove + "\nMovimento de canto = " + edgeScrolling;
        }
        if (Input.GetMouseButtonDown(1))
        {
            cameraCanMove = !cameraCanMove;
            debugText.text = "Debug(Pressione ESPAÇO ou o botão direito do mouse)\nMovimento de câmera = " + cameraCanMove + "\nMovimento de canto = " + edgeScrolling;
        }
        if (Input.GetMouseButtonUp(1))
        {
            cameraCanMove = !cameraCanMove;
            debugText.text = "Debug(Pressione ESPAÇO ou o botão direito do mouse)\nMovimento de câmera = " + cameraCanMove + "\nMovimento de canto = " + edgeScrolling;
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
