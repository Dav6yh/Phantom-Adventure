using UnityEngine;
using UnityEngine.UI;

public class Aviso : MonoBehaviour
{
    [Header("Aviso do Jogo/Objeto")]
    [TextArea]
    [SerializeField] private string avisoTexto;
    [Header("Sprite para aparecer junto com o texto")]
    [SerializeField] private Sprite spriteAviso;
    [SerializeField] private Color corAviso = Color.white;
    [Header("Aviso temporario?")]
    [SerializeField] private bool avisoTemporario = false;
    [SerializeField] private GameObject ButtonTeleporte;

    public string AvisoTexto()
    {
        return avisoTexto;
    }

    public Sprite SpriteAviso()
    {
        return spriteAviso;
    }

    public Color CorAviso()
    {
        return corAviso;
    }

    public void DefineTroca(Sprite s, string t, Color c)
    {
        spriteAviso = s;
        avisoTexto = t;
        corAviso = c;
    }

    public bool AvisoTemporario()
    {
        return avisoTemporario;
    }

}