using UnityEngine;

public class ProjectileShooter : Shooter
{
    protected override void Shoot()
    {
        Bullet currentBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        if (currentBullet != null)
            InitializeBullet(currentBullet);
    }
}