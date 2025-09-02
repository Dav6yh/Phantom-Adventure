using System;
using System.Collections;
using UnityEngine;

public class PlayerAtaque : MonoBehaviour
{
    [SerializeField] private GameObject ataquePrefab;
    [SerializeField] private Transform mira;
    [SerializeField] private float ataqueCooldown = 1f;
    [SerializeField] private int dano;

    public bool dirPlayer;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Atacar()
    {    
        animator.SetTrigger("Atacar");
        Instantiate(ataquePrefab, mira.position, Quaternion.identity);
        

    }

    public void Mira()
    {
        if (dirPlayer)
        {
            mira.localPosition = new Vector3(0.3f, 0f, 0f);
        }
        else
        {
            mira.localPosition = new Vector3(-0.3f, 0f, 0f);
        }
    }
}
