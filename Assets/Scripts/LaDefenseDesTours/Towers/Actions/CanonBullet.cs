using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

public class CanonBullet : Bullet
{
    private readonly float arcHeight = 7.0f;
    private Vector3 startPosition;
    private float totalDistance;
    private float startTime;

    protected virtual void HitTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, specialAbility);

        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                ApplyEffect();
            }
        }

        Release();
    }
    protected override void HandleTrajectory()
    {
        if (target == null)
        {
            Release();
            return;
        }

        if (startPosition == Vector3.zero)
        {
            startPosition = transform.position;
            totalDistance = Vector3.Distance(startPosition, targetCollider.bounds.center);
            startTime = Time.time;
        }

        float progressDistance = TrackProgress();

        Vector3 updatePosition = CalculateArcPosition(progressDistance);
        UpdateBulletPosition(updatePosition);

        if (progressDistance >= 1.0f)
            HitTarget();
    }
    private float TrackProgress()
    {
        float distanceCovered = (Time.time - startTime) * speed;
        return distanceCovered / totalDistance; // Si 1, on a atteint la cible (distanceCovered = totalDistance)
    }
    private Vector3 CalculateArcPosition(float progressDistance)
    {
        Vector3 targetCenter = targetCollider.bounds.center;
        Vector3 linearPosition = Vector3.Lerp(startPosition, targetCenter, progressDistance);
        float height = arcHeight * Mathf.Sin(progressDistance * Mathf.PI);

        return linearPosition + Vector3.up * height;
    }
    private void UpdateBulletPosition(Vector3 updatePosition)
    {
        transform.position = updatePosition;
        transform.LookAt(targetCollider.bounds.center);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        startPosition = Vector3.zero;
    }
}