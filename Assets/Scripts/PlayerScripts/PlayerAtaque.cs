using System;
using System.Collections;
using UnityEngine;

public class PlayerAtaque : MonoBehaviour
{
    [SerializeField] private GameObject ataquePrefab;
    [SerializeField] private Transform mira;
    [SerializeField] private float ataqueCooldown = 1f;
    [SerializeField] private int dano = 3;

    public bool dirPlayer;
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
        Mira();
    }

    public void Atacar()
    {
        animator.SetTrigger("Atacar");
        Instantiate(ataquePrefab, mira.position, Quaternion.identity);

    }


    public void Mira()
    {
        if (joystick.RetornaX() > 0)
        {
            mira.localPosition = new Vector3(0.3f, 0f, 0f);

            ataquePrefab.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (joystick.RetornaX() < 0)
        {
            mira.localPosition = new Vector3(-0.3f, 0f, 0f);
            ataquePrefab.GetComponent<SpriteRenderer>().flipX = true;
        }
       
    }

    public int GetDano()
    {
        return dano += 3;
    }
    public int PerderDano()
    {
       return dano;
    }
}
