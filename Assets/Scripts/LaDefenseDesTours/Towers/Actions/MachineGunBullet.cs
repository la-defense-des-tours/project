using UnityEngine;

public class MachineGunBullet : Bullet
{
    protected override void HandleTrajectory()
    {
        if (target == null)
        {
            Release();
            return;
        }

        Vector3 targetCenter = targetCollider.bounds.center;
        Vector3 direction = targetCenter - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(targetCenter);
    }
    protected override void HitTarget()
    {
        targetEnemy.TakeDamage(damage);
        Release();
    }
}