using UnityEngine;

public class LaserBullet : Bullet
{
    private LineRenderer laserLine;
    [SerializeField] private Material laserMaterial;
    [SerializeField] private float laserWidth = 0.25f;

    private void Awake()
    {
        laserLine = gameObject.AddComponent<LineRenderer>();
        laserLine.positionCount = 2;
        laserLine.startWidth = laserWidth;
        laserLine.endWidth = laserWidth;
        laserLine.material = laserMaterial;
        laserLine.useWorldSpace = true;
    }

    protected override void HandleTrajectory()
    {
        if (target == null)
        {
            laserLine.enabled = false;
            return;
        }

        laserLine.enabled = true;
        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, targetCollider.bounds.center);

        HitTarget();
    }

    protected virtual void HitTarget()
    {
        if (targetEnemy != null)
        {
            float damageThisFrame = damage * specialAbility * Time.deltaTime;
            targetEnemy.TakeDamage(damageThisFrame);
            ApplyEffect();
        }
    }
}