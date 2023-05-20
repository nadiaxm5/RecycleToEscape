using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    public AudioSource audioCandado;

    void Start()
    {
        StartCoroutine(SonidoCandado());
    }

    IEnumerator SonidoCandado()
    {
        yield return new WaitForSeconds(3f);
        audioCandado.Play();
    }
}
