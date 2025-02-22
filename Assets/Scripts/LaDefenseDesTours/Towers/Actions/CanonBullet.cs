using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

public class CanonBullet : Bullet
{
    private readonly float arcHeight = 7.0f;
    private Vector3 startPosition;
    private float totalDistance;
    private float startTime;

    protected override void HitTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, specialAbility);

        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    protected override void HandleTrajectory()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (startPosition == Vector3.zero)
        {
            startPosition = transform.position;
            totalDistance = Vector3.Distance(startPosition, targetCollider.bounds.center);
            startTime = Time.time;
        }

        float distanceCovered = (Time.time - startTime) * speed;
        float progressDistance = distanceCovered / totalDistance; // Si 1, on a atteint la cible

        Vector3 targetCenter = targetCollider.bounds.center;
        Vector3 linearPosition = Vector3.Lerp(startPosition, targetCenter, progressDistance);
        float height = arcHeight * Mathf.Sin(progressDistance * Mathf.PI);
        Vector3 arcPosition = linearPosition + Vector3.up * height;

        transform.position = arcPosition;
        transform.LookAt(targetCenter);

        if (progressDistance >= 1.0f)
            HitTarget();
    }
}