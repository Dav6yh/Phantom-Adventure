using System.Collections;
using UnityEngine;

public class Corte : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestruirCorte());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestruirCorte()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
