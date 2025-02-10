using UnityEngine;

public sealed class Player : MonoBehaviour
{
    private static Player Instance { get; set; }
    public string Name { get; set; } = "Han Solo";
    public double health { get; set; } = 1000;
    public double score { get; set; } = 0;
    public double currency { get; set; } = 2000;
    public bool isDead { get; set; } = false;

    private void Awake()
    {
        // Unity Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public static Player GetInstance()
    {
        if (Instance == null)
            Debug.LogError("Player instance should not be null");

        return Instance;
    }

    public void TakeDamage(double damage)
    {
        health -= damage;
        CheckHeatlh();
    }

    private void CheckHeatlh()
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}