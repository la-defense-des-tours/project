using UnityEngine;

public class LaserBullet : Bullet
{
    [Header("Laser Settings")]
    private LineRenderer laserLine;
    [SerializeField] private Material laserMaterial;

    private bool isApplyingDamage;
    private const string ENEMY_TAG = "Enemy";

    private void Start()
    {
        SetupLaserVisuals();
        isApplyingDamage = true;
    }

    private void SetupLaserVisuals()
    {
        laserLine = gameObject.AddComponent<LineRenderer>();
        laserLine.positionCount = 2;
        laserLine.startWidth = 0.1f;
        laserLine.endWidth = 0.1f;
        laserLine.material = laserMaterial;
        laserLine.useWorldSpace = true;
    }

    protected override void HandleTrajectory()
    {
        if (target == null || !target.CompareTag(ENEMY_TAG))
        {
            laserLine.enabled = false;
            Destroy(gameObject);
            return;
        }

        laserLine.enabled = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = targetCollider.bounds.center;

        laserLine.SetPosition(0, startPos);
        laserLine.SetPosition(1, endPos);

        if (isApplyingDamage && targetEnemy != null)
        {
            float dmg = (damage + specialAbility) * Time.deltaTime;
            targetEnemy.TakeDamage(dmg);
        }
    }

    protected override void HitTarget()
    {
        isApplyingDamage = false;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        isApplyingDamage = false;
    }
}