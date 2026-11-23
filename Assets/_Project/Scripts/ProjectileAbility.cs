using UnityEngine;

public class ProjectileAbility : MonoBehaviour
{
    [SerializeField] private ProjectilePool projectilePool;
    [SerializeField] private float cooldown = 1f;

    private PlayerMovement playerMovement;
    private float cooldownTimer;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            Shoot();
            cooldownTimer = cooldown;
        }
    }

    private void Shoot()
    {
        if (projectilePool == null || playerMovement == null)
            return;

        Projectile projectile = projectilePool.Get();

        projectile.transform.position = transform.position;
        projectile.Initialize(playerMovement.FacingDirection);
    }
}