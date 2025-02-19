using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using System;

public sealed class Player : MonoBehaviour
{
    private static Player Instance { get; set; }
    public string Name { get; set; } = "Han Solo";
    public float health { get; set; } = 1000;
    public float score { get; set; } = 0;
    public float currency { get; set; } = 2000;
    public bool isDead { get; set; } = false;

    public event Action OnPlayerDeath;
    private void Awake()
    {
        // Unity Singleton pattern
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple Player instances detected!");
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this; 
    }

    public static Player GetInstance()
    {
        // Singleton pattern - useful for testing
        if (Instance != null)
            Instance = FindFirstObjectByType<Player>();
        else
            Instance = new GameObject("Player").AddComponent<Player>();

        return Instance;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
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