using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3Controller : MonoBehaviour
{
    GameObject[] interactuableObjects;
    GameObject[] itemObjects;
    public GameObject key;

    private void Start()
    {
        interactuableObjects = GameObject.FindGameObjectsWithTag("Interactuable");
        itemObjects = GameObject.FindGameObjectsWithTag("Item");
        key = GameObject.FindGameObjectWithTag("Key3");
        Invoke("Ilumination", 0.2f); //Desactivar outline para que funcione bien
    }

    void Ilumination()
    {
        foreach (GameObject item in interactuableObjects)
        {
            item.GetComponent<Outline>().enabled = false;
        }

        foreach (GameObject item in itemObjects)
        {
            item.GetComponent<Outline>().enabled = false;
        }

        key.GetComponent<Outline>().enabled = false;
    }
}
