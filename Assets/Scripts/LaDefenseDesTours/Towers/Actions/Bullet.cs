using UnityEngine;
using UnityEngine.Pool;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

public abstract class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] protected float speed = 70f;
    [SerializeField] protected Material defaultMaterial;
    [SerializeField] protected Material fireMaterial;
    [SerializeField] protected Material lightningMaterial;
    [SerializeField] protected Material iceMaterial;
    [SerializeField] protected GameObject impactEffect;
    [SerializeField] protected float impactEffectDuration = 0.5f;
    protected float specialAbility { get; set; }
    protected float damage;


    [Header("Target Settings")]
    protected Transform target;
    protected Collider targetCollider;
    protected Enemy targetEnemy;

    private IObjectPool<Bullet> pool;
    protected string effectType;


    void Update()
    {
        ApplyEffectMaterial();

        if (target == null)
        {
            Release();
            return;
        }

        HandleTrajectory();
    }

    public void Initialize(Transform target, float damage, float specialAbility, string effectType)
    {
        this.target = target;
        this.damage = damage;
        this.specialAbility = specialAbility;
        this.effectType = effectType;
        Seek(target);
    }

    protected abstract void HandleTrajectory();
        
    protected abstract void SpawnImpactEffect();

    public void SetPool(IObjectPool<Bullet> _pool)
    {
        pool = _pool;
    }

    protected void Release()
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

    private void Seek(Transform _target)
    {
        if (_target != null)
        {
            targetCollider = _target.GetComponent<Collider>();
            targetEnemy = _target.GetComponent<Enemy>();
        }
    }

    protected virtual void ApplyEffectMaterial()
    {
        switch (effectType)
        {
            case "Fire":
                GetComponent<Renderer>().material = fireMaterial;
                break;
            case "Ice":
                GetComponent<Renderer>().material = iceMaterial;
                break;
            case "Lightning":
                GetComponent<Renderer>().material = lightningMaterial;
                break;
            default:
                GetComponent<Renderer>().material = defaultMaterial;
                break;
        }
    }
}