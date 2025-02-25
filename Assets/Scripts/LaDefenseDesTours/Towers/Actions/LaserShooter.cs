using UnityEngine;

public class LaserShooter : Shooter
{
    private Bullet currentLaser;

    protected override void Shoot()
    {
        if (currentLaser == null)
        {
            currentLaser = SpawnBullet();
            if (currentLaser != null)
            {
                currentLaser.transform.SetParent(firePoint);
                InitializeBullet(currentLaser);
            }
        }
    }

    protected override void UpdateTarget()
    {
        Transform oldTarget = target;
        base.UpdateTarget();

        if (target != oldTarget && currentLaser != null)
        {
            currentLaser.Release();
            currentLaser = null;
        }
    }

    private void OnDestroy()
    {
        if (currentLaser != null)
        {
            currentLaser.Release();
            currentLaser = null;
        }
    }
}