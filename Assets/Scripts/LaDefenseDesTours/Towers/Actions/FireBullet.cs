using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;


public class FireBullet : Bullet
{
    protected override void HitTarget()
    {
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(damage);
            targetEnemy.TransitionTo(new Burned()); // Apply Burned state
        }
        Destroy(gameObject);
    }
}