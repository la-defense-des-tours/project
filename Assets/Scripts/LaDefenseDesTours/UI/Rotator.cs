using UnityEngine;

namespace LaDefenseDesTours.UI
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationSpeed;
        [SerializeField] private bool clampRotation = false;
        [SerializeField] private Vector3 minRotation;
        [SerializeField] private Vector3 maxRotation;

        private Vector3 currentEulerAngles;

        private void Start()
        {
            currentEulerAngles = transform.localEulerAngles;
        }

        private void Update()
        {
            currentEulerAngles += rotationSpeed * Time.deltaTime;

            if (clampRotation)
                currentEulerAngles = ClampEulerAngles(currentEulerAngles);

            transform.localEulerAngles = currentEulerAngles;
        }

        private Vector3 ClampEulerAngles(Vector3 angles)
        {
            return new Vector3(
                Mathf.Clamp(NormalizeAngle(angles.x), minRotation.x, maxRotation.x),
                Mathf.Clamp(NormalizeAngle(angles.y), minRotation.y, maxRotation.y),
                Mathf.Clamp(NormalizeAngle(angles.z), minRotation.z, maxRotation.z)
            );
        }

        private float NormalizeAngle(float angle)
        {
            angle %= 360f;
            return angle < 0f ? angle + 360f : angle;
        }
    }
}