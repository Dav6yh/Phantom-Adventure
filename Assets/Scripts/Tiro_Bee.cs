using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Tiro_Bee : MonoBehaviour
{
    public float lifeTime = 3f;   // Tempo até desaparecer
    public int damage = 1;        // Dano no player

    [SerializeField] private Player player;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>().GetComponent<Player>();
    }
    void Start()
    {
        Destroy(gameObject, lifeTime); // Auto-destrói
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Aqui você pode chamar o script de vida do player
            player.GetDano();

            Destroy(gameObject); // Some ao colidir
        }

        if (collision.CompareTag("Ground")) // Se bater no chão
        {
            Destroy(gameObject);
        }
    }

}
