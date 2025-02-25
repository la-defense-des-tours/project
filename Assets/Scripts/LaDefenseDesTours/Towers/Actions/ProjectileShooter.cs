using UnityEngine;

public class ProjectileShooter : Shooter
{
    protected override void Shoot()
    {
        Bullet currentBullet = SpawnBullet();

        if (currentBullet != null)
            InitializeBullet(currentBullet);
    }
}