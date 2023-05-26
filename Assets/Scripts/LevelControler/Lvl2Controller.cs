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
    public Canvas canvas;
    public Camera camPrincipal;
    public Camera camLlave;

    private void Start()
    {
        camPrincipal.enabled = true;
        camLlave.enabled = false;
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
        totalTasks = 0;
        StartCoroutine(CambioCamara());
    }

    IEnumerator CambioCamara()
    {
        animKey.Play("Key2");
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
