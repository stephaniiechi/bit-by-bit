using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 50;

    private ObjectPool<Projectile> pool;

    private void Awake()
    {
        pool = new ObjectPool<Projectile>(
            createFunc: CreateProjectile,
            actionOnGet: projectile => projectile.gameObject.SetActive(true),
            actionOnRelease: projectile => projectile.gameObject.SetActive(false),
            actionOnDestroy: projectile => Destroy(projectile.gameObject),
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    private Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform);

        projectile.gameObject.SetActive(false);
        projectile.SetPool(pool);

        return projectile;
    }

    public Projectile Get()
    {
        return pool.Get();
    }
}