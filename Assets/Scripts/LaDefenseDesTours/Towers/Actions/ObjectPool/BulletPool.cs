using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }
    private Dictionary<string, ObjectPool<Bullet>> pools = new Dictionary<string, ObjectPool<Bullet>>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private ObjectPool<Bullet> CreatePool(Bullet prefab)
    {
        return new ObjectPool<Bullet>(
            createFunc: () =>
            {
                Bullet bullet = Instantiate(prefab);
                bullet.SetPool(pools[prefab.name]);
                return bullet;
            },
            actionOnGet: bullet => bullet.gameObject.SetActive(true),
            actionOnRelease: bullet => bullet.gameObject.SetActive(false),
            actionOnDestroy: bullet => Destroy(bullet.gameObject),
            defaultCapacity: 20,
            maxSize: 200
        );
    }

    public Bullet GetBullet(Bullet bullet)
    {
        string poolKey = bullet.name;

        if (!pools.ContainsKey(poolKey))
            pools[poolKey] = CreatePool(bullet);

        return pools[poolKey].Get();
    }
}