using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorder = 10f;
    private readonly Vector3 moveForward = new Vector3(1, 0, 0);
    private readonly Vector3 moveBackward = new Vector3(-1, 0, 0);
    private readonly Vector3 moveLeft = new Vector3(0, 0, 1);
    private readonly Vector3 moveRight = new Vector3(0, 0, -1);
    
    void Update()
    {
        HandleCameraMovement();
    }

    void MoveCamera(Vector3 direction)
    {
        transform.Translate(direction * panSpeed * Time.deltaTime, Space.World);
    }

    void HandleCameraMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorder)
        {
            MoveCamera(moveForward);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorder)
        {
            MoveCamera(moveBackward);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorder)
        {
            MoveCamera(moveLeft);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorder)
        {
            MoveCamera(moveRight);
        }
    }
}
