using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [Header("Tower Attributes")]
    [SerializeField] private float rotatingSpeed = 7f;
    [SerializeField] private Transform rotatingPart;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireCountdown;

    private const string ENEMY_TAG = "Enemy";
    public Transform target;
    private float range;

    [Header("Bullet Attributes")]
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected Transform firePoint;
    private float damage;
    private float specialAbility;

    [SerializeField] private float lockOnAngle = 5f;
        private bool isLockedOn;

    [Header("Bullet Attributes")]
    [SerializeField] private Bullet bulletPrefab; // Base bullet prefab

    private string effectType; // New field to store the effect type

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
        {
            isLockedOn = false;
            return;
        }


        LockOnTarget();

        Vector3 direction = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotatingPart.rotation, targetRotation, Time.deltaTime * rotatingSpeed).eulerAngles;
        rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        float angle = Quaternion.Angle(rotatingPart.rotation, targetRotation);
        isLockedOn = angle <= lockOnAngle;

        if (isLockedOn && fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
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

    private void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * rotatingSpeed).eulerAngles;
        rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void SetSpecialAbility(float _specialAbility)
    {
        specialAbility = _specialAbility;
    }

    protected virtual void Shoot()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (bulletInstance != null)
            InitializeBullet(bulletInstance);
    }

    public void InitializeBullet(Bullet bullet)
    {
        bullet.Seek(target);
        bullet.SetDamage(damage);
        bullet.SetSpecialAbility(specialAbility);

        // Set the effect type based on the tower's effect
        switch (effectType)
        {
            case "Fire":
                if (bullet is FireBullet fireBullet)
                    fireBullet.SetEffectType(effectType);
                break;
            case "Ice":
                if (bullet is IceBullet iceBullet)
                    iceBullet.SetEffectType(effectType);
                break;
            case "Lightning":
                if (bullet is LightningBullet lightningBullet)
                    lightningBullet.SetEffectType(effectType);
                break;
        }
    }

    public void SetRange(float _range)
    {
        range = _range;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public void SetEffectType(string _effectType) // New method to set the effect type
    {
        effectType = _effectType;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}