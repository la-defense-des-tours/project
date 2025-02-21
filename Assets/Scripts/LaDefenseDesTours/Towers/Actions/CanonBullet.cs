using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

public class CanonBullet : Bullet
{
    protected override void HitTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, specialAbility);

        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}