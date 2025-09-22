using System.Collections;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private int vida = 3;
    [SerializeField] private PlayerAtaque playerDano;
    private Rigidbody2D rb;

    [Header("Movimento")]
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;
    private Transform targetPoint;

    [Header("Ataque")]
    public GameObject projectilPrefab;
    public Transform firePoint;
    public float fireRate = 2f;

    private float nextFireTime;
    private Transform player;   // Refer�ncia do player quando entra no trigger

    private Animator animator;


    private int dir = 1;
    [SerializeField] private float beeSpeed;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private VirtualJoystick2D playerJoy;

    
    void Awake()
    {
        playerJoy = FindAnyObjectByType<VirtualJoystick2D>();
        playerDano = FindAnyObjectByType<PlayerAtaque>();
    }
    void Start()
    {
        targetPoint = pointB;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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



    private void Shoot()
    {
        
        if (projectilPrefab == null || firePoint == null) return;
        StartCoroutine(ShootCoroutine());

    }

    IEnumerator ShootCoroutine()
        {
            animator.SetTrigger("Tiro");
            yield return new WaitForSeconds(0.5f);
            GameObject projectile = Instantiate(projectilPrefab, firePoint.position, Quaternion.identity);

            // Dire��o em rela��o ao player
            Vector2 direction = (player.position - firePoint.position).normalized;
            projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * 10f;

            // Ajusta a rota����o do proj��til para olhar na dire����o do movimento
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));// Tempo de delay para o tiro
        }
    

    // Detecta quando o player entra no trigger do inimigo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
             if (collision.gameObject.CompareTag("Corte"))
        {
            Vector3 direction = -(collision.transform.position - this.transform.position).normalized;
            StartCoroutine(KnockbackCoroutine(direction));
            vida -= playerDano.GetDano();
            animator.SetTrigger("Hit");
            if (vida <= 0)
            {
                StartCoroutine(Morrer());
            }
        }   
    }
    private IEnumerator KnockbackCoroutine(Vector3 direction)
    {
        yield return new WaitForSeconds(0.1f); // Pequeno atraso antes do knockback
        float knockbackDuration = 0.2f; // Dura????o do knockback
        float knockbackForce = 10f; // Força do knockback
        float timer = 0f;
        while (timer < knockbackDuration)
        {
            rb.linearVelocity = direction * knockbackForce;
            timer += Time.deltaTime;
            yield return null;
        }
        rb.linearVelocity = Vector2.zero; // Para o movimento ap??s o knockback
    }

    // Detecta quando o player sai do trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
        }
    }

    IEnumerator Morrer()
    {
        //audioSource.TocarSom(somMorte);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}

