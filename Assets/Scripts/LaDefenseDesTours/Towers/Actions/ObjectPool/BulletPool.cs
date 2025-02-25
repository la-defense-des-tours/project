using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }
    private ObjectPool<Bullet> pool;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Initialize(Bullet prefab)
    {
        pool = new ObjectPool<Bullet>(
            createFunc: () =>
            {
                Bullet bullet = Instantiate(prefab);
                bullet.SetPool(pool);
                return bullet;
            },
            actionOnGet: bullet => bullet.gameObject.SetActive(true),
            actionOnRelease: bullet => bullet.gameObject.SetActive(false),
            actionOnDestroy: bullet => Destroy(bullet.gameObject),
            defaultCapacity: 20,
            maxSize: 100
        );
    }

    public Bullet GetBullet(Bullet prefab)
    {
        if (pool == null)
            Initialize(prefab);
        return pool.Get();
    }
}