using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 75f;
    private Transform target;
    private Collider targetCollider;
    private float damage;

    void Update()
    {
        HandleTrajectory();
    }

    public void Seek(Transform _target)
    {
        target = _target;
        targetCollider = _target.GetComponent<Collider>();
    }

    private void HitTarget(Enemy enemy)
    {
        Destroy(gameObject);
        // Add damage to target
        enemy.TakeDamage(50);
    }

    private void HandleTrajectory()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetCenter = targetCollider.bounds.center;
        Vector3 direction = targetCenter - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget(target.GetComponent<Enemy>());
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
}
