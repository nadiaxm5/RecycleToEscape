using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithDrawer : MonoBehaviour
{
    private bool abierto;
    private Vector3 posicionInicial;
    public AudioSource openDrawer;
    public AudioSource closeDrawer;

    void Start()
    {
        abierto = false;
        posicionInicial = transform.position;
        openDrawer = GameObject.Find("AbrirCajon").GetComponent<AudioSource>();
        closeDrawer = GameObject.Find("CerrarCajon").GetComponent<AudioSource>();
    }

    public void DrawerInteraction()
    {
        StartCoroutine("OpenTheDrawer");
    }

    private IEnumerator OpenTheDrawer()
    {
        if (!abierto)
        {
            openDrawer.Play();
            for (float i = 0f; i <= 1.7f; i+= 0.1f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x - 0.1f,
                    transform.localPosition.y,
                    transform.localPosition.z);
                yield return new WaitForSeconds(0f);
            }
            abierto = true;
        }
        else
        {
            closeDrawer.Play();
            for (float i = 1.7f; i >= 0f; i -= 0.1f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x + 0.1f,
                    transform.localPosition.y,
                    transform.localPosition.z);
                yield return new WaitForSeconds(0f);
            }
            abierto = false;
            transform.position = posicionInicial;
        }
    }
}
