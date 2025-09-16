using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    [SerializeField] private int vida = 4; // Health of the player
    [SerializeField] private int dano;

    [SerializeField] private float moveH;

    [Header("Vida")]
    [SerializeField] private GameObject coracao1;
    [SerializeField] private GameObject coracao2;
    [SerializeField] private GameObject coracao3;
    [SerializeField] private GameObject coracao4;
    [SerializeField] private Rigidbody2D rb;

    //[SerializeField] private GameObject coracaoExtra;
    //private bool coracaoAparecer = false;

    private VirtualJoystick2D joystick;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        joystick = FindAnyObjectByType<VirtualJoystick2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Vida();
    }

    private void Mover()
    {

        if (joystick.RetornaX() > 0)
        {
            animator.SetBool("Andando", true);
            animator.SetTrigger("Andar");
            spriteRenderer.flipX = false;
        }
        else if (joystick.RetornaX() < 0)
        {
            animator.SetBool("Andando", true);
            animator.SetTrigger("Parar");
            spriteRenderer.flipX = true;

        }
        else
        {
            animator.SetBool("Andando", false);
            animator.SetTrigger("Parar");
        }
    }



    private void Vida()
    {
        if (vida == 4)
        {
            coracao1.SetActive(true);
            coracao2.SetActive(true);
            coracao3.SetActive(true);
            coracao4.SetActive(true);
        }
        else if (vida == 3)
        {
            coracao1.SetActive(false);
            coracao2.SetActive(true);
            coracao3.SetActive(true);
            coracao4.SetActive(true);
        }
        else if (vida == 2)
        {
            coracao1.SetActive(false);
            coracao2.SetActive(false);
            coracao3.SetActive(true);
            coracao4.SetActive(true);
        }
        else if (vida == 1)
        {
            coracao1.SetActive(false);
            coracao2.SetActive(false);
            coracao3.SetActive(false);
            coracao4.SetActive(true);
        }
        else
        {
            coracao1.SetActive(false);
            coracao2.SetActive(false);
            coracao3.SetActive(false);
            coracao4.SetActive(false);
        }

        //VidaExtra();
    }

    private void TomarDano()
    {

        StartCoroutine(TomarDanoCoroutine());
    }

    IEnumerator TomarDanoCoroutine()
    {
        vida -= 1;
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(0.5f);
        if (vida <= 0)
        {
            Morrer();
            // Aqui você pode adicionar lógica para reiniciar o nível ou mostrar uma tela de game over
        }
    }

    private void Morrer()
    {
        animator.SetTrigger("Morrer");
        //Vibrar
        Handheld.Vibrate();
        // Desabilitar o controle do jogador
        this.enabled = false;
    }

    //private void VidaExtra()
    //{
    //    if (coracaoAparecer = true)
    //    {
    //        coracaoExtra.SetActive(true);
    //        vida +=1;
    //    }
    //    else if (vida >= 4) 
    //    {
    //        coracaoExtra.SetActive(false);
    //        coracaoAparecer = false;
    //    }
    //}
    public int GetVida()
    {
        return vida += 1;
    }

    public int PerderVida()
    {
        return vida -= 1;
    }

    public void GetDano()
    {
        TomarDano();
    }
    //public bool GetCoracaoExtra()
    //{
    //    return coracaoAparecer;
    //}

    public float GetMoveH()
    {
        return moveH;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo") || collision.gameObject.CompareTag("TiroInimigo"))
        {
            Vector3 direction = -(collision.transform.position - this.transform.position).normalized;
            TomarDano();
            StartCoroutine(KnockbackCoroutine(direction));
        }
    }

    private IEnumerator KnockbackCoroutine(Vector3 direction)
    {
        yield return new WaitForSeconds(0.1f); // Pequeno atraso antes do knockback
        float knockbackDuration = 0.2f; // Dura????o do knockback
        float knockbackForce = 7f; // Força do knockback
        float timer = 0f;
        while (timer < knockbackDuration)
        {
            rb.linearVelocity = direction * knockbackForce;
            timer += Time.deltaTime;
            yield return null;
        }
        rb.linearVelocity = Vector2.zero; // Para o movimento ap??s o knockback
    }
}

