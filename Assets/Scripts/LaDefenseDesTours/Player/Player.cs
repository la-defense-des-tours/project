using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using System;
using Assets.Scripts.LaDefenseDesTours;
using Assets.Scripts.LaDefenseDesTours.Game;

public sealed class Player : MonoBehaviour, Health
{
    private static Player Instance { get; set; }
    public string Name { get; set; } 
    public float health { get; set; }
    public float maxHealth { get; set; } = 2000;
    public float score { get; set; } = 0;
    public float currency { get; set; } = 2000;
    public bool isDead { get; set; } = false;

    public event Action OnPlayerDeath;

    public event Action OnHealthChanged;

    public HealthBar healthBar;

    public int wavesSurvived { get; private set; } = 0;
    private void Awake()
    {
        // Unity Singleton pattern
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple Player instances detected!");
            DestroyImmediate(gameObject);
            return;
        }

        Name = PlayerPrefs.GetString("PlayerName", "Han Solo");

        healthBar = FindFirstObjectByType<HealthBar>();
        if (healthBar != null)
        {
            healthBar.SetTarget(this);
        }

        health = maxHealth;
        Instance = this;
    }

    public static Player GetInstance()
    {
        if (Instance == null)
        {
            Instance = FindFirstObjectByType<Player>();
            if (Instance == null)
                Instance = new GameObject("Player").AddComponent<Player>();
        }

        if (Instance.health == 0)
            Instance.health = Instance.maxHealth;

        return Instance;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        OnHealthChanged?.Invoke();
        CheckHeatlh();
    }

    public void UpgradeLife()
    {
        maxHealth += 100;
        health = maxHealth;
        OnHealthChanged?.Invoke();

    }
    private void CheckHeatlh()
    {
        if (health <= 0)
        {
            healthBar.gameObject.SetActive(false);
            isDead = true;
            OnPlayerDeath?.Invoke();
            Debug.Log("Player is dead");
        }
    }
    public void IncrementWavesSurvived()
    {
        wavesSurvived++;
    }

    public void OnWaveCompleted()
    {
        IncrementWavesSurvived();
    }
}