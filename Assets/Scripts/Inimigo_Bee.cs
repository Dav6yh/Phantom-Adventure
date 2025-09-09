using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;
    private Transform targetPoint;

    [Header("Ataque")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;

    private float nextFireTime;
    private Transform player;   // Refer�ncia do player quando entra no trigger

    void Start()
    {
        targetPoint = pointB;
    }

    void Update()
    {
        Patrol();

        if (player != null && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Patrol()
    {
        if (pointA == null || pointB == null) return;

        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Dire��o em rela��o ao player
        Vector2 direction = (player.position - firePoint.position).normalized;
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * 5f;
    }

    // Detecta quando o player entra no trigger do inimigo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
        }
    }

    // Detecta quando o player sai do trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
        }
    }
}
