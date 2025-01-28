using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool disableMovement = false;
    [SerializeField] private float panSpeed = 30f;
    [SerializeField] private float panBorder = 10f;
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float minY = 35f;
    [SerializeField] private float maxY = 75f;

    private readonly Vector3 moveForward = new(1, 0, 0);
    private readonly Vector3 moveBackward = new(-1, 0, 0);
    private readonly Vector3 moveLeft = new(0, 0, 1);
    private readonly Vector3 moveRight = new(0, 0, -1);
    private bool doMovement = true;
    
    void Update()
    {
        if (disableMovement)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
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
        transform.Translate(panSpeed * Time.deltaTime * direction, Space.World);
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
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
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
