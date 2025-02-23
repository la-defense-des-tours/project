using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;


public class LightningBullet : Bullet
{
    protected override void HitTarget()
    {
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(damage);
            targetEnemy.TransitionTo(new Paralyzed());
        }
        Destroy(gameObject);
    }
}