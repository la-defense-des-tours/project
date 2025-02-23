using Assets.Scripts.LaDefenseDesTours.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.LaDefenseDesTours
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image healthFill;

        private Health target;
        private float fadeSpeed = 2f;

        public void SetTarget(Health healthTarget)
        {
            target = healthTarget;

            if (target != null)
            {
                healthSlider.maxValue = target.maxHealth;
                healthSlider.value = target.health;
                HandleHealthBarVisibility();
                target.OnHealthChanged += UpdateHealthBar; 
            }
        }

        private void OnDestroy()
        {
            if (target != null)
                target.OnHealthChanged -= UpdateHealthBar; 
        }

        private void UpdateHealthBar()
        {
            if (target == null) return;

            UpdateHealthColor();
            healthSlider.value = target.health;

            gameObject.SetActive(true);
            if (target.health <= 0)
            {
                gameObject.SetActive(false);

            }
            canvasGroup.alpha = 1f;
        }

        private void UpdateHealthColor()
        {
            if (healthFill == null) return;

            float healthPercentage = target.health / target.maxHealth;

            Color newColor = Color.Lerp(Color.red, Color.green, healthPercentage);
            healthFill.color = newColor;
        }

        public void HandleHealthBarVisibility()
        {
            bool shouldBeVisible = target.health < target.maxHealth && target.health > 0;

            Debug.Log("maxHealth " + target.maxHealth);
            Debug.Log("health" + target.health);

            if (shouldBeVisible)
            {
                gameObject.SetActive(true);
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1f, Time.deltaTime * fadeSpeed);
            }
            else
            {
                canvasGroup.alpha = 0f;
                gameObject.SetActive(false); 
            }
        }
    }
}

