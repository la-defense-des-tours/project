using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Tower Attributes")]
    [SerializeField] private float rotatingSpeed = 7f;
    [SerializeField] private Transform rotatingPart;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireCountdown;
    [SerializeField] private float lockOnAngle = 5f;
    private const string ENEMY_TAG = "Enemy";
    private Transform target;
    private float range;
    private bool isLockedOn;

    [Header("Bullet Attributes")]
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform firePoint;
    private float damage;
    private float specialAbility;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        RotateTurret();
    }

    void RotateTurret()
    {
        if (target == null)
        {
            isLockedOn = false;
            return;
        }

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

    void UpdateTarget()
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

    void Shoot()
    {
        Bullet bulletInstance = Instantiate(bullet, firePoint.position, firePoint.rotation);
        if (bulletInstance != null)
            InitializeBullet(bulletInstance);
    }

    private void InitializeBullet(Bullet bullet)
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