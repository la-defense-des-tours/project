using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
public class Shooter : MonoBehaviour
{
    [Header("Tower Attributes")]
    [SerializeField] private float rotatingSpeed = 7f;
    [SerializeField] private Transform rotatingPart;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireCountdown;
    
    [Header("Bullet Attributes")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private Transform target;
    private float range;
    private float damage;
    private string enemyTag = "Enemy";

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        
        Tower tower = GetComponent<Tower>();
        range = tower.range;
        damage = tower.damage;
    }

    void Update()
    {
        RotateTurret();
    }

    void RotateTurret()
    {
        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * rotatingSpeed).eulerAngles;
        rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    void Shoot()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, bulletPrefab.transform.rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
            bullet.SetDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
