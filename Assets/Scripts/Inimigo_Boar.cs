using System.Collections;
using UnityEngine;

public class Inimigo_Boar : MonoBehaviour
{
    [SerializeField] private int vida = 6;
    [SerializeField] private PlayerAtaque playerDano;
    [SerializeField] private Rigidbody2D rb;

    private int dir = 1;
    [SerializeField] private float speed;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    //private Audio audioSource;
    //[SerializeField] private AudioClip somMorte;

    private void Awake()
    {
        playerDano = FindAnyObjectByType<PlayerAtaque>().GetComponent<PlayerAtaque>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Patrulha();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        

        //audioSource = GetComponent<Audio>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(dir * speed * Time.deltaTime, 0, 0);
        animator.SetTrigger("Correr");


    }
    void Patrulha()
    {
        if (dir == 1)
        {
            dir = -1;

        }
        else
        {
            dir = 1;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Direita"))
        {
            Patrulha();
            spriteRenderer.flipX = false;
        }
        else if (collision.gameObject.CompareTag("Esquerda"))
        {
            Patrulha();
            spriteRenderer.flipX = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Corte"))
        {
            Vector3 direction = -(collision.transform.position - this.transform.position).normalized;
            StartCoroutine(KnockbackCoroutine(direction));
            vida -= playerDano.GetDano();
            animator.SetTrigger("TomarDano");
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
    IEnumerator Morrer()
    {
        //audioSource.TocarSom(somMorte);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}

