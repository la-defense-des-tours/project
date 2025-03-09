using UnityEngine;

public class ProjectileShooter : Shooter
{
    protected override void Shoot()
    {
        Bullet spawnedBullet = SpawnBullet();
        spawnedBullet.Initialize(target, damage, specialAbility, effectType);
    }
}