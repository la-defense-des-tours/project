using UnityEngine;
using System.Collections;

public class LaserBullet : Bullet
{
    [Header("Laser Settings")]
    private LineRenderer laserLine;
    [SerializeField] private Material laserMaterial;

    private bool isApplyingDamage;
    private float lastDamage;

    private void Start()
    {
        SetupLaserVisuals();
        isApplyingDamage = true;
    }

    private void SetupLaserVisuals()
    {
        if (laserLine == null)
        {
            laserLine = gameObject.AddComponent<LineRenderer>();
            laserLine.positionCount = 2;
            laserLine.startWidth = 0.1f;
            laserLine.endWidth = 0.1f;
            laserLine.material = laserMaterial;
        }
    }

    protected override void HandleTrajectory()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, targetCollider.bounds.center);

        if (isApplyingDamage && targetEnemy != null)
        {
            float baseDmg = damage * Time.deltaTime;
            float dotDmg = specialAbility * Time.deltaTime;
            lastDamage = baseDmg + dotDmg;

            targetEnemy.TakeDamage(lastDamage);
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

    private void OnGUI()
    {
        if (isApplyingDamage && targetEnemy != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(targetCollider.bounds.center);
            GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y, 100, 20), 
                     $"DPS: {(damage + specialAbility):F1}");
        }
    }
}