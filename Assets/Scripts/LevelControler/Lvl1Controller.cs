using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1Controller : MonoBehaviour
{
    public int wasteRecycled = 0;
    private int totalWaste;
    public Transform waste;
    public Animator animKey;
    public GameObject[] interactuableObjects;
    public GameObject[] itemObjects;
    public AudioSource audiolevelUp;
    public Canvas canvas;
    public Camera camPrincipal;
    public Camera camLlave;

    private void Start()
    {
        camPrincipal.enabled = true;
        camLlave.enabled = false;
        totalWaste = waste.childCount; //Saber el número de objetos residuos
        Invoke("Ilumination", 0.3f); //Desactivar outline para que funcione bien
    }

    void Update()
    {
        if(wasteRecycled == totalWaste) //Cuando se reciclan todos los deshechos
        {
            DarLlave2();
        }
    }

    void Ilumination()
    {
        foreach (GameObject item in itemObjects)
        {
            if (item.GetComponent<Outline>() != null)
            {
                item.GetComponent<Outline>().enabled = false;
            }   
        }

        foreach (GameObject item in interactuableObjects)
        {
            if(item.GetComponent<Outline>() != null)
            {
                item.GetComponent<Outline>().enabled = false;
            }
        }
    }

    void DarLlave2()
    {
        audiolevelUp.Play();
        wasteRecycled = 0;
        StartCoroutine(CambioCamara());
    }

    IEnumerator CambioCamara()
    {
        animKey.Play("Key1");
        yield return new WaitForSeconds(0.1f);
        canvas.enabled = false;
        camPrincipal.enabled = false;
        camLlave.enabled = true;
        yield return new WaitForSeconds(3f);
        canvas.enabled = true;
        camLlave.enabled = false;
        camPrincipal.enabled = true;
        StopAllCoroutines();
    }
}
