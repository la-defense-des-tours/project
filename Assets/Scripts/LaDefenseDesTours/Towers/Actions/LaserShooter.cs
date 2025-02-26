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
            Destroy(currentLaser.gameObject);
            currentLaser = null;
        }
    }

    public override void SetEffectType(string _effectType)
    {
        base.SetEffectType(_effectType);

        if (currentLaser != null)
        {
            InitializeBullet(currentLaser);
        }
    }

    private void OnDestroy()
    {
        if (currentLaser != null)
            Destroy(currentLaser.gameObject);
    }
}