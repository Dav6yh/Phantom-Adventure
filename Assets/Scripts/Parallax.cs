using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxFactor;

    private Vector3 previousCameraPosition;

    void Start()
    {
        previousCameraPosition = Camera.main.transform.position;
    }

    void Update()
    {
        Vector3 delta = Camera.main.transform.position - previousCameraPosition;
        transform.position += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor, 0);
        previousCameraPosition = Camera.main.transform.position;
    }
}
