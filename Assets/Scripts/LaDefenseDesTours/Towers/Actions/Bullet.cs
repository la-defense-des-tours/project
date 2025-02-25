using UnityEngine;
using UnityEngine.Pool;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

public abstract class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] protected float speed = 70f;
    protected virtual float specialAbility { get; set; }
    protected float damage;

    [Header("Target Settings")]
    protected Transform target;
    protected Collider targetCollider;
    protected Enemy targetEnemy;

    private IObjectPool<Bullet> pool;

    void Update()
    {
        if (target == null)
        {
            Release();
            return;
        }
        HandleTrajectory();
    }

    public void Seek(Transform _target)
    {
        target = _target;
        targetCollider = _target.GetComponent<Collider>();
        targetEnemy = _target.GetComponent<Enemy>();
    }

    protected abstract void HitTarget();

    protected abstract void HandleTrajectory();

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public void SetSpecialAbility(float _specialAbility)
    {
        specialAbility = _specialAbility;
    }

    public void SetPool(IObjectPool<Bullet> _pool)
    {
        pool = _pool;
    }

    public void Release()
    {
        if (pool != null)
            pool.Release(this);
        else
            Destroy(gameObject);
    }

    protected virtual void OnDisable()
    {
        target = null;
        targetCollider = null;
        targetEnemy = null;
        damage = 0;
        specialAbility = 0;
    }
}