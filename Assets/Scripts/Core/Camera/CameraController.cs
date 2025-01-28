using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool disableMovement = false;
    [SerializeField] private float panSpeed = 30f;
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float minY = 35f;
    [SerializeField] private float maxY = 75f;
    [SerializeField] private GameObject mapObject;

    private Vector3 moveForward = new(1, 0, 0);
    private Vector3 moveBackward = new(-1, 0, 0);
    private Vector3 moveLeft = new(0, 0, 1);
    private Vector3 moveRight = new(0, 0, -1);
    private Vector3 mapMinBounds;
    private Vector3 mapMaxBounds;
    private bool doMovement = true;

    void Start()
    {
        if (mapObject.TryGetComponent(out Collider collider))
        {
            Bounds bounds = collider.bounds;
            mapMinBounds = bounds.min;
            mapMaxBounds = bounds.max;
        }
        else
        {
            Debug.LogError("Map Object must have a Renderer or Collider to calculate boundaries.");
        }
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

    void MoveCamera(Vector3 direction)
    {
        transform.Translate(panSpeed * Time.deltaTime * direction, Space.World);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, mapMinBounds.x, mapMaxBounds.x);
        pos.z = Mathf.Clamp(pos.z, mapMinBounds.z, mapMaxBounds.z);
        transform.position = pos;
    }

    void HandleCameraMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - 10)
            MoveCamera(moveForward);
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= 10)
            MoveCamera(moveBackward);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= 10)
            MoveCamera(moveLeft);
        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - 10)
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
        Debug.Log(doMovement ? "Camera movement enabled" : "Camera movement disabled");
    }
}
