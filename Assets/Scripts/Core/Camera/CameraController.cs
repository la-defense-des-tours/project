using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool disableMovement = false;
    [SerializeField] private float panSpeed = 30f;
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private GameObject cameraBoundary;

    private Camera cam;
    private Bounds mapBounds;
    private bool doMovement = true;
    private readonly float minZoom = 35f;
    private readonly float maxZoom = 75f;
    private readonly Vector3 forward = new(1, 0, 0);
    private readonly Vector3 back = new(-1, 0, 0);
    private readonly Vector3 left = new(0, 0, 1);
    private readonly Vector3 right = new(0, 0, -1);

    void Start()
    {
        cam = Camera.main;
        if (cameraBoundary.TryGetComponent(out Collider collider))
            mapBounds = collider.bounds;
        else
            Debug.LogError("Map Object must have a Collider to calculate boundaries.");
    }

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

    void HandleCameraMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - 10)
            moveDirection += forward;
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= 10)
            moveDirection += back;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= 10)
            moveDirection += left;
        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - 10)
            moveDirection += right;

        if (moveDirection != Vector3.zero)
            MoveCamera(moveDirection.normalized);
    }

    void MoveCamera(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + panSpeed * Time.deltaTime * direction;
        targetPosition = ClampCameraPosition(targetPosition);
        transform.position = targetPosition;
    }

    Vector3 ClampCameraPosition(Vector3 targetPosition)
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        float x = Mathf.Clamp(targetPosition.x, mapBounds.min.x + halfHeight, mapBounds.max.x - halfHeight);
        float z = Mathf.Clamp(targetPosition.z, mapBounds.min.z + halfWidth, mapBounds.max.z - halfWidth);

        targetPosition = new Vector3(x, targetPosition.y, z);

        return targetPosition;
    }

    void HandleCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            Vector3 pos = transform.position;
            pos.y = Mathf.Clamp(pos.y - scroll * 1000 * scrollSpeed * Time.deltaTime, minZoom, maxZoom);
            transform.position = ClampCameraPosition(pos);
        }
    }
    void DebugCameraInfo()
    {
        Debug.Log(doMovement ? "Camera movement enabled" : "Camera movement disabled");
    }
}
