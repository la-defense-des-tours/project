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

    protected virtual void HitTarget()
    {
        SpawnImpactEffect();
        targetEnemy.TakeDamage(damage);
        ApplyEffect();
        Release();
    }

    protected override void SpawnImpactEffect()
    {
        if (impactEffect == null)
            return;

        GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        if (ps != null)
            SetupParticles(ps);
 
        Destroy(effect, impactEffectDuration);
    }

    private void SetupParticles(ParticleSystem ps)
    {
        ps.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
    }
}