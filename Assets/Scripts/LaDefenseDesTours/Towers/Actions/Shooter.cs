using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

public abstract class Shooter : MonoBehaviour
{
    [Header("Tower Attributes")]
    [SerializeField] private float rotatingSpeed = 7f;
    [SerializeField] private Transform rotatingPart;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireCountdown;
    protected Transform target;
    private float range;
    private const string ENEMY_TAG = "Enemy";

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
    
    public void Initialize(float range, float damage, float specialAbility, string effectType)
    {
        this.range = range;
        this.damage = damage;
        this.specialAbility = specialAbility;
        this.effectType = effectType;
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

    private bool HasLineOfSight()
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
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float shortestDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;

        foreach (Enemy enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && nearestEnemy.gameObject.CompareTag(ENEMY_TAG))
            target = nearestEnemy.transform;
        else
            target = null;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}