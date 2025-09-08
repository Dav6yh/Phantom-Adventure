//using System.Collections;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.Rendering.Universal;

//public class EfeitoVida : MonoBehaviour
//{
//    [SerializeField] private Player player;
//    [SerializeField] private PlayerController2D playerController2D;
//    [SerializeField] private PlayerAtaque playerAtaque;

//    [SerializeField] private float duracaoEfeito;
//    [SerializeField] private bool efeitoAtivo = false;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        player = GetComponent<Player>();
//        playerController2D = GetComponent<PlayerController2D>();
//        playerAtaque = GetComponent<PlayerAtaque>();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("VidaUp"))
//        {
//            if (efeitoAtivo == false)
//            {
                
//                efeitoAtivo = true;

//                // Corrigido para usar uma variável temporária para armazenar a velocidade
//                bool v = player.GetCoracaoExtra() == true;

                
//                Destroy(collision.gameObject);
//            }
//        }
//    }

//    private void Temporizador()
//    {
//        StartCoroutine(TempoEfeito());
//    }

//    IEnumerator TempoEfeito()
//    {
//        while (duracaoEfeito >= 0)
//        {
//            yield return new WaitForSeconds(1f);
//            duracaoEfeito--;
//        }

//        if (duracaoEfeito <= 0)
//        {
//            duracaoEfeito = 5;
//            efeitoAtivo = false;
//            playerController2D.PerderSpeed();
//        }
//    }
//}

