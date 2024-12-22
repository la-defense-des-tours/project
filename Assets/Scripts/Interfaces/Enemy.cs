using UnityEngine;
using UnityEngine.AI;

public interface Enemy
{
    public void SetupNavMeshAgent();
    public void Move(Vector3 destination);
    public Enemy Clone();
    public void TakeDamage(float damage);
    public void Die();
}