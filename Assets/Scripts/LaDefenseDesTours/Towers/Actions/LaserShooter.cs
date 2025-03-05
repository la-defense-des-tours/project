using UnityEngine;

public class LaserShooter : Shooter
{
    private Bullet currentLaser;

    protected override void Shoot()
    {
        if (currentLaser == null)
        {
            currentLaser = Instantiate(bullet, firePoint.position, firePoint.rotation);
            if (currentLaser != null)
            {
                currentLaser.transform.SetParent(firePoint);
                currentLaser.Initialize(target, damage, specialAbility, effectType);
            }
        }
        else
        {
            currentLaser.Initialize(target, damage, specialAbility, effectType);
        }
    }

    protected override void UpdateTarget()
    {
        Transform oldTarget = target;
        base.UpdateTarget();
        if (target != oldTarget && currentLaser)
        {
            Destroy(currentLaser.gameObject);
            currentLaser = null;
        }
        if (currentLaser && (!target || !HasLineOfSight()))
        {
            Destroy(currentLaser.gameObject);
            currentLaser = null;
        }
    }

    private void OnDestroy()
    {
        if (currentLaser != null)
            Destroy(currentLaser.gameObject);
    }
}