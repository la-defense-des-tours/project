using Assets.Scripts.LaDefenseDesTours.UI.HUD;
using UnityEngine;

namespace Core.Camera
{
    public class DayNightController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Light sun;

        [SerializeField] private ParticleSystem dustParticles;

        [Header("Cycle Settings")]
        [SerializeField]
        private float dayDuration;

        [Range(0.1f, 0.9f)][SerializeField] private float nightTransitionThreshold;

        [Header("Lighting Settings")]
        [SerializeField]
        private Color nightColor;

        [SerializeField] private Color duskColor;
        private Color dayColor;

        private Quaternion startRotation;
        private float currentTime;
        private bool isDay;

        void Start()
        {
            startRotation = sun.transform.rotation;
            dayColor = sun.color;
        }

        void Update()
        {
            UpdateSun();
            ControlEnvironmentalEffects();
        }

        private void UpdateSun()
        {
            currentTime = GameUI.instance.gameTimer;
            float normalizedTime = currentTime % dayDuration / dayDuration;

            RotateSun(normalizedTime);
            UpdateLighting(normalizedTime);
        }

        private void RotateSun(float normalizedTime)
        {
            Quaternion targetRotation = startRotation * Quaternion.Euler(normalizedTime * 360f, 0f, 0f);
            sun.transform.rotation = targetRotation;
        }

        private void UpdateLighting(float normalizedTime)
        {
            if (normalizedTime < nightTransitionThreshold)
                ApplyDayLighting(normalizedTime);
            else
                ApplyNightLighting(normalizedTime);
        }

        private void ApplyDayLighting(float normalizedTime)
        {
            float dayProgress = normalizedTime / nightTransitionThreshold;

            sun.color = Color.Lerp(dayColor, duskColor, Mathf.SmoothStep(0, 1, dayProgress));
            isDay = true;
        }

        private void ApplyNightLighting(float normalizedTime)
        {
            float nightProgress = (normalizedTime - nightTransitionThreshold) / (1 - nightTransitionThreshold);

            if (nightProgress < 0.5f)
            {
                float duskToNightProgress = nightProgress * 2f;
                sun.color = Color.Lerp(duskColor, nightColor, Mathf.SmoothStep(0, 1, duskToNightProgress));
                isDay = false;
            }
            else
            {
                float nightToDawnProgress = (nightProgress - 0.5f) * 2f; 
                sun.color = Color.Lerp(nightColor, duskColor, Mathf.SmoothStep(0, 1, nightToDawnProgress));
            }
        }

        private void ControlEnvironmentalEffects()
        {
            if (isDay)
                dustParticles.Play();
            else
                dustParticles.Stop();
        }

        public bool IsDay()
        {
            return isDay;
        }
    }
}