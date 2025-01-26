using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 30f;
    [SerializeField] private float panBorder = 10f;
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float minY = 20f;
    [SerializeField] private float maxY = 60f;

    private readonly Vector3 moveForward = new Vector3(1, 0, 0);
    private readonly Vector3 moveBackward = new Vector3(-1, 0, 0);
    private readonly Vector3 moveLeft = new Vector3(0, 0, 1);
    private readonly Vector3 moveRight = new Vector3(0, 0, -1);
    private bool doMovement = true;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DebugCameraInfo();
            doMovement = !doMovement;
        }

        if (!doMovement)
            return;

        HandleCameraMovement();
        HandleCameraZoom();
    }

    void MoveCamera(Vector3 direction)
    {
        transform.Translate(direction * panSpeed * Time.deltaTime, Space.World);
    }

    void HandleCameraMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorder)
            MoveCamera(moveForward);
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorder)
            MoveCamera(moveBackward);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorder)
            MoveCamera(moveLeft);
        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorder)
            MoveCamera(moveRight);
    }

    void HandleCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        transform.position = pos;
    }

    void DebugCameraInfo()
    {
        if (doMovement)
            Debug.Log("Camera movement enabled");
        else
            Debug.Log("Camera movement disabled");
    }
}
