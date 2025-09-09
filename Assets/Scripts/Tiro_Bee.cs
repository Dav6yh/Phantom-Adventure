using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Tiro_Bee : MonoBehaviour
{
    public float lifeTime = 3f;   // Tempo at� desaparecer
    public int damage = 1;        // Dano no player

    [SerializeField] private Player player;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>().GetComponent<Player>();
    }
    void Start()
    {
        Destroy(gameObject, lifeTime); // Auto-destr�i
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Aqui voc� pode chamar o script de vida do player
            player.GetDano();

            Destroy(gameObject); // Some ao colidir
        }

        if (collision.CompareTag("Ground")) // Se bater no ch�o
        {
            Destroy(gameObject);
        }
    }

}
