using UnityEngine;

public class ParallaxGroup : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // A câmera que segue o jogador
    [SerializeField][Range(0f, 1f)] private float baseParallax = 0.5f;
    [SerializeField] private bool affectVertical = false;

    private Vector3 lastCameraPosition;
    private Transform[] layers;
    private float[] parallaxScales;

    private void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        lastCameraPosition = cameraTransform.position;

        // Pega todos os filhos (as camadas do fundo)
        int count = transform.childCount;
        layers = new Transform[count];
        parallaxScales = new float[count];

        for (int i = 0; i < count; i++)
        {
            layers[i] = transform.GetChild(i);
            // Cada camada recebe um multiplicador baseado na ordem (camadas mais no fundo movem menos)
            parallaxScales[i] = baseParallax * ((float)i / count);
        }
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        for (int i = 0; i < layers.Length; i++)
        {
            float multiplier = parallaxScales[i];
            Vector3 move = new Vector3(deltaMovement.x * multiplier, affectVertical ? deltaMovement.y * multiplier : 0, 0);
            layers[i].position += move;
        }

        lastCameraPosition = cameraTransform.position;
    }
}
