using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithHood : MonoBehaviour
{
    private bool abierto;
    private Vector3 posicionInicial;
    public AudioSource openHood;
    public AudioSource closeHood;

    void Start()
    {
        abierto = false;
        posicionInicial = transform.position;
        openHood = GameObject.Find("AbrirCapo").GetComponent<AudioSource>();
        closeHood = GameObject.Find("CerrarCapo").GetComponent<AudioSource>();
    }

    public void HoodInteracion()
    {
        StartCoroutine("OpenTheHood");
    }

    private IEnumerator OpenTheHood()
    {
        if (!abierto)
        {
            openHood.Play();
            for (float i = -90f; i >= -180f; i -= 5f)
            {
                transform.localRotation = Quaternion.Euler(i, 0f, 0f);
                yield return new WaitForSeconds(0f);
            }
            abierto = true;
        }
        else
        {
            closeHood.Play();
            for (float i = -180f; i <= -90f; i += 5f)
            {
                transform.localRotation = Quaternion.Euler(i, 0f, 0f);
                yield return new WaitForSeconds(0f);
            }
            abierto = false;
            transform.position = posicionInicial;
        }
    }
}
