using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using System;
using Assets.Scripts.LaDefenseDesTours;

public sealed class Player : MonoBehaviour, Health
{
    private static Player Instance { get; set; }
    public string Name { get; set; } = "Han Solo";
    public float health { get; set; }
    public float maxHealth { get; set; } = 1000;
    public float score { get; set; } = 0;
    public float currency { get; set; } = 2000;
    public bool isDead { get; set; } = false;

    public event Action OnPlayerDeath;

    public event Action OnHealthChanged;
    private void Awake()
    {
        // Unity Singleton pattern
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple Player instances detected!");
            DestroyImmediate(gameObject);
            return;
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
    private void CheckHeatlh()
    {
        if (health <= 0)
        {
            isDead = true;
            OnPlayerDeath?.Invoke();
            Debug.Log("Player is dead");
        }
    }
}