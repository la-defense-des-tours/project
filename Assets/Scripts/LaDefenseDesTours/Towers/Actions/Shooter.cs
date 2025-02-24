using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [Header("Tower Attributes")]
    [SerializeField] private float rotatingSpeed = 7f;
    [SerializeField] private Transform rotatingPart;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireCountdown;
    private const string ENEMY_TAG = "Enemy";
    protected Transform target;
    private float range;

    [Header("Bullet Attributes")]
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected Transform firePoint;
    private float damage;
    private float specialAbility;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        RotateTurret();
    }

    private void RotateTurret()
    {
        if (target == null)
            return;

        LockOnTarget();

        if (fireCountdown <= 0f && HasLineOfSight())
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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    protected abstract void Shoot();

    private void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * rotatingSpeed).eulerAngles;
        rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    protected void InitializeBullet(Bullet bullet)
    {
        bullet.Seek(target);
        bullet.SetDamage(damage);
        bullet.SetSpecialAbility(specialAbility);
    }

    public void SetRange(float _range)
    {
        range = _range;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public void SetSpecialAbility(float _specialAbility)
    {
        specialAbility = _specialAbility;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}