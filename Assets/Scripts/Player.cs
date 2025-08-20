using UnityEngine;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    [SerializeField] private int vida = 4; // Health of the player
    [SerializeField] private int dano;
    [SerializeField] private float speedwalk;
    [SerializeField] private float speedrun;
    [SerializeField] private float forcejump;
    [SerializeField] private float moveH;
    [SerializeField] private bool noChao = true;
    [SerializeField] private int stelf = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Jump();
    }

    private void Mover()
    {

        moveH = Input.GetAxis("Horizontal");
        transform.position += new Vector3(moveH * Time.deltaTime * speedwalk, 0, 0);

    }

    private void Jump()
    {
      
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcejump), ForceMode2D.Impulse);
        noChao = false;
        
    }

}
