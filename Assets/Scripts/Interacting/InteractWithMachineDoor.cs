using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithMachineDoor : MonoBehaviour
{
    public AudioSource open;

    void Start()
    {
        open = GameObject.Find("AbrirCapo").GetComponent<AudioSource>();
    }

    public void MachineDoorInteracion()
    {
        StartCoroutine("OpenTheMachineDoor");
    }

    private IEnumerator OpenTheMachineDoor()
    {
        open.Play();
        for (float i = 0f; i <= 90f; i += 5f)
        {
            transform.localRotation = Quaternion.Euler(-90f, i, -90f);
            yield return new WaitForSeconds(0f);
        }
    }
}
