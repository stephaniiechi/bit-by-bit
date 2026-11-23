using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifetime = 3f;

    private Vector2 direction;
    private IObjectPool<Projectile> pool;
    private float lifetimeTimer;
    private bool isActive;

    public void SetPool(IObjectPool<Projectile> projectilePool)
    {
        pool = projectilePool;
    }

    public void Initialize(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
        lifetimeTimer = lifetime;
        isActive = true;
    }

    private void Update()
    {
        if (!isActive)
            return;

        transform.position +=
            (Vector3)(direction * speed * Time.deltaTime);

        lifetimeTimer -= Time.deltaTime;

        if (lifetimeTimer <= 0f)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        isActive = false;

        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}