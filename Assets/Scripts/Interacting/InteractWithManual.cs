using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithManual : MonoBehaviour
{
    public GameObject panelManual;
    [SerializeField]
    private PlayerScript playerScript;
    public bool manualActivo = false;

    private void Start()
    {
        panelManual.SetActive(false);
        playerScript = FindObjectOfType<PlayerScript>();
    }

    public void AbrirManual()
    {
        panelManual.SetActive(true);
        manualActivo = true;
        playerScript.speedCam = 0; //Que no se mueva el fondo
        Cursor.visible = true; //Hacer que el cursor esté activo
        Cursor.lockState = CursorLockMode.None;
    }

    public void CerrarManual()
    {
        panelManual.SetActive(false);
        manualActivo = false;
        playerScript.speedCam = 300;
        Cursor.visible = false; //Hacer que el cursor no esté activo
        Cursor.lockState = CursorLockMode.Locked;
    }
}
