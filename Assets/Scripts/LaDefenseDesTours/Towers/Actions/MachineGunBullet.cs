public class MachineGunBullet : Bullet
{
    protected override void HitTarget()
    {
        Destroy(gameObject);
        targetEnemy.TakeDamage(damage);
        ApplyEffect();
    }
}