using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    [SerializeField] private int vida = 4; // Health of the player
    [SerializeField] private int dano;
    //[SerializeField] private float speedwalk;
    //[SerializeField] private float speedrun;
    [SerializeField] private float forcejump;
    [SerializeField] private float moveH;
    [SerializeField] private bool noChao = true;
    [SerializeField] private int stelf = 3;

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
        Jump();
        if (noChao == true)
        {
            animator.SetBool("NoChao", true);
            
        }

    }

    private void Mover()
    {

        if (joystick.RetornaX() > 0)
        {
            animator.SetBool("Andando", false);
            animator.SetBool("NoChao", true);
            animator.SetTrigger("Andar");
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (joystick.RetornaX() < 0)
        {
            animator.SetBool("Andando", false);
            animator.SetBool("NoChao", true);
            animator.SetTrigger("Parar");
            GetComponent<SpriteRenderer>().flipX = true;

        }
        else
        {
            animator.SetBool("Andando", true);
            animator.SetBool("NoChao", true);
            animator.SetTrigger("Parar");
        }
    }

    private void Jump()
    {
      
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcejump), ForceMode2D.Impulse);
        noChao = false;
        
    }

}
