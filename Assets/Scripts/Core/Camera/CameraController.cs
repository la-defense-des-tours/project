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
    private readonly float minZoom = 40f;
    private readonly float maxZoom = 60f;
    private readonly Vector3 forward = new(1, 0, 0);
    private readonly Vector3 back = new(-1, 0, 0);
    private readonly Vector3 left = new(0, 0, 1);
    private readonly Vector3 right = new(0, 0, -1);
    private Vector3 touchDelta;  
    private Vector3 velocity = Vector3.zero;  
    private float damping = 5f;


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
        HandleTouchInput();
    }
    void HandleTouchInput()
    {
        if (Input.touchCount == 1) // Glissement avec un doigt
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                // Calcul du mouvement dans la direction opposée pour le drag
                touchDelta = new Vector3(-touch.deltaPosition.x, 0, -touch.deltaPosition.y) * 0.02f;
                MoveCamera(touchDelta);
                velocity = touchDelta; // Sauvegarde pour l'inertie
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Appliquer l’inertie quand on lève le doigt
                velocity = touchDelta;
            }
        }
        else if (Input.touchCount == 2) // Zoom avec deux doigts
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Distance précédente
            float prevDistance = (touch0.position - touch0.deltaPosition - (touch1.position - touch1.deltaPosition)).magnitude;
            // Distance actuelle
            float currentDistance = (touch0.position - touch1.position).magnitude;

            float difference = (currentDistance - prevDistance) * 0.1f; // Ajuste la sensibilité
            HandlePinchZoom(difference);
        }

        if (velocity.magnitude > 0.01f)
        {
            MoveCamera(velocity);
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * damping);
        }
    }


    void HandlePinchZoom(float delta)
    {
        Vector3 pos = transform.position;

        pos.y -= delta * scrollSpeed * 2f;

        pos.y = Mathf.Clamp(pos.y, minZoom, maxZoom);

        transform.position = ClampCameraPosition(pos);
    }


    void HandleCameraMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveDirection += GetMovementDirection(KeyCode.Z);
        if (Input.GetKey(KeyCode.A)) moveDirection += GetMovementDirection(KeyCode.Q);
        if (Input.GetKey(KeyCode.S)) moveDirection += GetMovementDirection(KeyCode.S);
        if (Input.GetKey(KeyCode.D)) moveDirection += GetMovementDirection(KeyCode.D);

        // Mouvements avec la souris
        if (Input.mousePosition.y >= Screen.height - 10) moveDirection += forward;
        if (Input.mousePosition.y <= 10) moveDirection += back;
        if (Input.mousePosition.x <= 10) moveDirection += left;
        if (Input.mousePosition.x >= Screen.width - 10) moveDirection += right;

        if (moveDirection != Vector3.zero)
            MoveCamera(moveDirection.normalized);
    }

    private Vector3 GetMovementDirection(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Z:
                return forward;
            case KeyCode.S:
                return back;
            case KeyCode.Q:
                return left;
            case KeyCode.D:
                return right;
            default:
                return Vector3.zero;
        }
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
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minZoom, maxZoom);

        transform.position = ClampCameraPosition(pos);
    }
    void DebugCameraInfo()
    {
        Debug.Log(doMovement ? "Camera movement enabled" : "Camera movement disabled");
    }
}
