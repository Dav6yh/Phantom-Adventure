using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Tiro_Bee : MonoBehaviour
{
    public float lifeTime = 1f;   // Tempo até desaparecer
    public int damage = 1;        // Dano no player

    [SerializeField] private Player player;

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }
    void Start()
    {
        Destroy(gameObject, lifeTime); // Auto-destrói
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))  // Se bater no chão
        {
            Destroy(gameObject);
        }
    }
}

