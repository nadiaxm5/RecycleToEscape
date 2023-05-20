using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour
{
    public AudioSource audioMachine;
    public Animator cubeTriturado;
    public Animator cubePeletizado;
    public GameObject piezaAMover;
    public AudioSource audiolevelUp;
    [SerializeField]
    private InteractWithMachineDoor machineDoor;

    private void Start()
    {
        machineDoor = FindObjectOfType<InteractWithMachineDoor>();
    }

    public void StartAnimation()
    {
        audioMachine.Play();
        StartCoroutine("ChangeCube");
    }

    private IEnumerator ChangeCube()
    {
        if(cubeTriturado != null) //Trituradora
        {
            piezaAMover.GetComponent<Animator>().Play("MoverPieza");
            yield return new WaitForSeconds(2f);
            cubeTriturado.Play("CuboTriturado");

        }
        else if (cubePeletizado != null) //Peletizadora
        {
            yield return new WaitForSeconds(2f);
            cubePeletizado.Play("CuboPeletizado");
        }
        else //Inyectora
        {
            piezaAMover.GetComponent<Animator>().Play("MoverPieza");
            yield return new WaitForSeconds(2f);
            machineDoor.MachineDoorInteracion();
            audiolevelUp.Play();
        }
    }
}
