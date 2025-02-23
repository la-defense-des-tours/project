using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;


public class IceBullet : Bullet
{
    protected override void HitTarget()
    {
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(damage);
            targetEnemy.TransitionTo(new Slowed());
        }
        Destroy(gameObject);
    }
}