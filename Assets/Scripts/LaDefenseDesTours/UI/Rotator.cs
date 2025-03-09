using UnityEngine;

namespace LaDefenseDesTours.UI
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationSpeed;
        [SerializeField] private bool clampRotation = false;
        [SerializeField] private Vector3 minRotationDelta;
        [SerializeField] private Vector3 maxRotationDelta;

        private Vector3 initialEulerAngles;
        private Vector3 rotationProgress;
        private Vector3 rotationDirection = Vector3.one;

        private void Start()
        {
            initialEulerAngles = transform.localEulerAngles;
            rotationProgress = Vector3.zero;
        }

        private void Update()
        {
            if (clampRotation)
                OscillateRotation();
            else
                RotateContinuously();
        }

        private void RotateContinuously()
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
        }

        private void OscillateRotation()
        {
            UpdateRotationProgress();
            Vector3 newRotation = new Vector3(
                Mathf.Lerp(minRotationDelta.x, maxRotationDelta.x, rotationProgress.x),
                Mathf.Lerp(minRotationDelta.y, maxRotationDelta.y, rotationProgress.y),
                Mathf.Lerp(minRotationDelta.z, maxRotationDelta.z, rotationProgress.z)
            );

            transform.localEulerAngles = initialEulerAngles + newRotation;
        }

        private void UpdateRotationProgress()
        {
            for (int axis = 0; axis < 3; axis++)
            {
                float speed = rotationSpeed[axis] * Time.deltaTime;

                rotationProgress[axis] += speed * rotationDirection[axis];

                if (rotationProgress[axis] >= 1f)
                {
                    rotationProgress[axis] = 1f;
                    rotationDirection[axis] = -1f;
                }
                else if (rotationProgress[axis] <= 0f)
                {
                    rotationProgress[axis] = 0f;
                    rotationDirection[axis] = 1f;
                }
            }
        }
    }
}
