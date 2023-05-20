using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithMotorOil : MonoBehaviour
{
    public GameObject llaveAllenAnimada;
    public Animator animAllen;
    public ParticleSystem motorOil;
    public AudioSource oilSpilled;

    public void MotorOilInteraction()
    {
        StartCoroutine("RemoveTheMotorOil");
        
    }

    private IEnumerator RemoveTheMotorOil()
    {
        llaveAllenAnimada.SetActive(true);
        yield return new WaitForSeconds(2f);
        llaveAllenAnimada.SetActive(false);
        StartCoroutine("MotorOilFalling");
    }

    private IEnumerator MotorOilFalling()
    {
        motorOil.Play();
        yield return new WaitForSeconds(0.2f);
        oilSpilled.Play();
        yield return new WaitForSeconds(4f);
        motorOil.Stop();
        yield return new WaitForSeconds(3f);
        oilSpilled.Stop();
    }
}
