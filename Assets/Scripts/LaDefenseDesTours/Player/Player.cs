using UnityEngine;

public sealed class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public string Name { get; set; }
    public double health { get; set; }
    public double score { get; set; }
    public double currency { get; set; }

    private Player()
    {
        Name = "Han Solo";
        health = 1000;
        score = 0;
        currency = 0;
        Debug.Log($"Player created: {Name} - {health} - {score} - {currency}");
    }
    private void Update()
    {
        CheckHeatlh();
    }
    public static Player GetInstance()
    {
        if (Instance == null)
            Instance = new Player();

        return Instance;
    }

    public void TakeDamage(double damage)
    {
        health -= damage;
    }

    private void CheckHeatlh()
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}