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

    private void Start()
    {
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
        animKey.Play("Key1");
        wasteRecycled = 0;
    }
}
