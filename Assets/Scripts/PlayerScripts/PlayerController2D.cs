using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    public VirtualJoystick2D joystick;
    public float moveSpeed = 5f;

    [Header("Jump Settings")]
    [SerializeField] private float forcaPulo;
    [SerializeField] private bool noPiso = true;
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Buttons")]
    public Button jumpButton;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();   
    }

    void FixedUpdate()
    {
        Vector2 input = joystick != null ? joystick.InputDirection : new Vector2(Input.GetAxis("Horizontal"), 0);

        //rb.linearVelocity = input * moveSpeed;

        transform.position += new Vector3(input.x * moveSpeed * Time.deltaTime, 0, 0) ;
    }

    public void Pular()
    {
        if(noPiso == true)
        {
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            //rb.linearVelocity *= forcaPulo;
            animator.SetBool("NoChao", false);
            animator.SetTrigger("Pular");
            animator.SetBool("Pulando", true);
            animator.SetBool("NoAr", true);
            noPiso = false;
        }
        else if (noPiso == false)
        {
            animator.SetBool("Pulando", false);
            animator.SetBool("NoAr", false);

        }
      

    }

    //public void OnJumpButtonPressed()
    //{
    //    if (!apertado)
    //    {
    //        Pular();
    //        apertado = true;
    //    }
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            noPiso = true;
            animator.SetBool("NoChao", true);
            animator.SetBool("Pulando", false);
            animator.SetBool("NoAr", false);
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            noPiso = true;
            animator.SetBool("NoChao", true); 
        }
    }
}
