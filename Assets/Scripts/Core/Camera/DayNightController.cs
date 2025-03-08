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
        private float dayDuration = 300f;

        [Header("Lighting Settings")]

        private Gradient lightingGradient;

        private Quaternion startRotation;
        private float currentTime;
        private bool isDay;

        void Start()
        {
            startRotation = sun.transform.rotation;
            lightingGradient = SetupDefaultGradient(); 
        }

        void Update()
        {
            UpdateSun();
            ControlEnvironmentalEffects();
        }

        private void UpdateSun()
        {
            currentTime = GameUI.instance.gameTimer;
            float normalizedTime = (currentTime % dayDuration) / dayDuration;
            Quaternion targetRotation = startRotation * Quaternion.Euler(normalizedTime * 360f, 0f, 0f);
            sun.transform.rotation = targetRotation;
            sun.color = lightingGradient.Evaluate(normalizedTime);
        }

        private void ControlEnvironmentalEffects()
        {
            float normalizedTime = (GameUI.instance.gameTimer % dayDuration) / dayDuration;
            bool newIsDay = normalizedTime < 0.5f;

            if (newIsDay == isDay)
                return;
            
            isDay = newIsDay;
            
            if (isDay)
                dustParticles.Play();
            else
                dustParticles.Stop();
        }

        private Gradient SetupDefaultGradient()
        {
            Gradient gradient = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[4];
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];

            colorKeys[0] = new GradientColorKey(sun.color, 0f);
            colorKeys[1] = new GradientColorKey(new Color(1f, 0.5f, 0f), 0.3f);
            colorKeys[2] = new GradientColorKey(new Color(0f, 0f, 0.5f), 0.65f);
            colorKeys[3] = new GradientColorKey(sun.color, 1f);

            alphaKeys[0] = new GradientAlphaKey(1f, 0f);
            alphaKeys[1] = new GradientAlphaKey(1f, 1f);

            gradient.SetKeys(colorKeys, alphaKeys);
            return gradient;
        }

        public bool IsDay()
        {
            return isDay;
        }
    }
}