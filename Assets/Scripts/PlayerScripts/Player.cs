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

    private VirtualJoystick2D joystick;

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        joystick = GameObject.Find("Background").GetComponent<VirtualJoystick2D>();
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
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (joystick.RetornaX() < 0)
        {
            animator.SetBool("Andando", true);
            animator.SetTrigger("Parar");
            GetComponent<SpriteRenderer>().flipX = true;

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
    }

    public int GetVida()
    {
        return vida += 1;
    }

    public int PerderVida()
    {
        return vida -= 1;
    }
}
