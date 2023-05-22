using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithDownButton : MonoBehaviour
{
    [SerializeField]
    private InteractWithUpButton upButton;
    public AudioSource elevatorAudio;
    public GameObject coche;
    private bool moviendoCoche;

    void Start()
    {
        moviendoCoche = false;
        upButton = FindObjectOfType<InteractWithUpButton>();
    }

    public void ButtonInteraction()
    {
        if (!moviendoCoche)
        {
            StartCoroutine("ElevateTheCar");
        }
    }

    private IEnumerator ElevateTheCar()
    {
        moviendoCoche = true;
        if (upButton.estaArriba)
        {
            elevatorAudio.Play();
            for (float i = 2.5f; i >= 0.4; i -= 0.005f)
            {
                coche.transform.localPosition = new Vector3(coche.transform.localPosition.x,
                    coche.transform.localPosition.y - 0.005f,
                    coche.transform.localPosition.z);
                yield return new WaitForSeconds(0f);
            }
            upButton.estaArriba = false;
        }
        moviendoCoche = false;
    }
}
