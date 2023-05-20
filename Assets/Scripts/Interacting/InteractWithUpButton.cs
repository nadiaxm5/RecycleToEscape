using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithUpButton: MonoBehaviour
{
    public bool estaArriba;
    public AudioSource elevatorAudio;
    public GameObject coche;

    void Start()
    {
        estaArriba = false;
    }

    public void ButtonInteraction()
    {
        StartCoroutine("ElevateTheCar");
    }

    private IEnumerator ElevateTheCar()
    {
        if (!estaArriba)
        {
            elevatorAudio.Play();
            for (float i = 0.4f; i <= 2.5f; i += 0.005f)
            {
                coche.transform.localPosition = new Vector3(coche.transform.localPosition.x,
                    coche.transform.localPosition.y + 0.005f,
                    coche.transform.localPosition.z);
                yield return new WaitForSeconds(0f);
            }
            estaArriba = true;
        }
    }
}