using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class Inimigo_Boar : MonoBehaviour
{
    [SerializeField] private int vida = 6;
    [SerializeField] private PlayerAtaque playerDano;

    private int dir = 1;
    [SerializeField] private float speed;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    //private Audio audioSource;
    //[SerializeField] private AudioClip somMorte;

    private void Awake()
    {
        playerDano = FindAnyObjectByType<PlayerAtaque>().GetComponent<PlayerAtaque>();
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
            vida -= playerDano.GetDano();
            animator.SetTrigger("TomarDano");
            if (vida <= 0)
            {
                StartCoroutine(Morrer());
            }
        }
    }

    IEnumerator Morrer()
    {
        animator.SetTrigger("Morrer");
        //audioSource.TocarSom(somMorte);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}

