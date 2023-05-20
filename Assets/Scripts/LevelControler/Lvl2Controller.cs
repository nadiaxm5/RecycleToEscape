using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2Controller : MonoBehaviour
{
    private int totalTasks = 3;
    public int tasksDone = 0;
    public int wheelsOut = 0;
    public GameObject[] interactuableObjects;
    public GameObject[] itemObjects;
    public AudioSource audiolevelUp;
    public Animator animKey;

    private void Start()
    {
        Invoke("Ilumination", 0.3f); //Desactivar outline para que funcione bien
    }

    void Update()
    {
        if (tasksDone == totalTasks) //Cuando se realizan todas las tareas
        {
            DarLlave1();
        }
    }

    void Ilumination()
    {
        foreach (GameObject item in itemObjects)
        {
            item.GetComponent<Outline>().enabled = false;
        }

        foreach (GameObject item in interactuableObjects)
        {
            item.GetComponent<Outline>().enabled = false;
        }
    }

    void DarLlave1()
    {
        audiolevelUp.Play();
        animKey.Play("Key2");
        totalTasks = 0;
    }
}
