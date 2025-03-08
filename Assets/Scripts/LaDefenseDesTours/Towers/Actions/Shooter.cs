using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;
using LaDefenseDesTours.Strategy;

public abstract class Shooter : MonoBehaviour
{
    [Header("Tower Attributes")]
    [SerializeField] private float rotatingSpeed = 7f;
    [SerializeField] private Transform rotatingPart;
    [SerializeField] private float fireCountdown;
    protected Transform target;
    private IStrategy strategy;
    private float range;
    private const string ENEMY_TAG = "Enemy";
    private float fireRate;
    [Header("Bullet Attributes")]
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected Transform firePoint;
    protected float damage;
    protected float specialAbility;
    protected string effectType;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        if (FindFirstObjectByType<BulletPool>() == null)
        {
            GameObject poolObj = new GameObject("BulletPool");
            poolObj.AddComponent<BulletPool>();
        }
    }

    private void Update()
    {
        RotateTurret();
    }

    public void Initialize(float range, float damage, float specialAbility, string effectType, IStrategy strategy, float fireRate)
    {
        this.range = range;
        this.damage = damage;
        this.specialAbility = specialAbility;
        this.effectType = effectType;
        this.strategy = strategy ?? new NearestEnemy();
        this.fireRate = fireRate;
    }

    private void RotateTurret()
    {
        if (target == null || !HasLineOfSight())
            return;

        LockOnTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    protected bool HasLineOfSight()
    {
        if (target == null)
            return false;

        Collider targetCollider = target.GetComponent<Collider>();
        if (targetCollider == null)
            return false;

        Vector3 targetPosition = targetCollider.bounds.center;
        LayerMask environmentLayer = LayerMask.GetMask("Environment");

        return !Physics.Linecast(firePoint.position, targetPosition, environmentLayer);
    }

    protected virtual void UpdateTarget()
    {
        Transform oldTarget = target;
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        if (strategy != null)
        {
            Enemy selectedEnemy = strategy.SelectTarget(enemies, transform.position, range);
            Transform newTarget = selectedEnemy != null ? selectedEnemy.transform : null;

            if (newTarget != null || target == null || !IsTargetValid(target))
            {
                target = newTarget;
                if (target != null && target != oldTarget)
                    fireCountdown = 0f;
            }
        }
        else if (target == null || !IsTargetValid(target))
        {
            target = null;
        }
    }

    private bool IsTargetValid(Transform checkTarget)
    {
        if (checkTarget == null)
            return false;

        Enemy enemy = checkTarget.GetComponent<Enemy>();
        if (enemy == null || enemy.tag != ENEMY_TAG)
            return false;

        float distance = Vector3.Distance(transform.position, checkTarget.position);
        if (distance > range)
            return false;

        return true;
    }

    private void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * rotatingSpeed).eulerAngles;
        rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    protected abstract void Shoot();

    protected Bullet SpawnBullet()
    {
        Bullet spawnedBullet = BulletPool.Instance.GetBullet(bullet);
        spawnedBullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
        return spawnedBullet;
    }
}