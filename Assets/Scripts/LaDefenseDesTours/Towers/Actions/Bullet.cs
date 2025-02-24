using UnityEngine;
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

    protected string effectType;


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

    protected virtual void HandleTrajectory()
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

    public void SetEffectType(string _effectType)
    {
        effectType = _effectType;
    }

    protected void ApplyEffect()
    {
        if (targetEnemy != null)
        {
            switch (effectType)
            {
                case "Fire":
                    targetEnemy.TransitionTo(new Burned());
                    break;
                case "Ice":
                    targetEnemy.TransitionTo(new Slowed());
                    break;
                case "Lightning":
                    targetEnemy.TransitionTo(new Paralyzed());
                    break;
            }
        }
    }
}