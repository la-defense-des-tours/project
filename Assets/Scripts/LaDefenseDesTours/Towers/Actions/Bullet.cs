using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

public abstract class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] protected float speed = 75f;
    protected virtual float specialAbility { get; set; }
    protected float damage;

    [Header("Target Settings")]
    protected Transform target;
    protected Collider targetCollider;
    protected Enemy targetEnemy;

    void Update()
    {
        HandleTrajectory();
    }

    public void Seek(Transform _target)
    {
        target = _target;
        targetCollider = _target.GetComponent<Collider>();
        targetEnemy = _target.GetComponent<Enemy>();
    }

    protected abstract void HitTarget();

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
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(targetCenter);
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public void SetSpecialAbility(float _specialAbility)
    {
        specialAbility = _specialAbility;
    }
}