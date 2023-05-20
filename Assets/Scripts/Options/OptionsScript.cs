using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour
{
    public GameObject panelOpciones;
    public bool panelActivo;
    public GameObject panelControles;

    [SerializeField]
    private PlayerScript playerScript;
    [SerializeField]
    public Inventory inventory;
    [SerializeField]
    public InteractWithManual manualScript;

    // Start is called before the first frame update
    void Start()
    {
        manualScript = FindObjectOfType<InteractWithManual>();
        playerScript = FindObjectOfType<PlayerScript>();
        inventory = FindObjectOfType<Inventory>();
        panelOpciones.SetActive(false);
        panelControles.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panelActivo = !panelActivo; //Si está abierto lo cierro y vicebersa
        }
        if (panelActivo)
        {
            panelOpciones.SetActive(true);
            playerScript.speedCam = 0; //Que no se mueva el fondo
            Cursor.visible = true; //Hacer que el cursor esté activo
            Cursor.lockState = CursorLockMode.None;
        }
        else if(!panelActivo && !inventory.inventoryEnabled && !manualScript.manualActivo)
        {
            panelOpciones.SetActive(false);
            panelControles.SetActive(false);
            playerScript.speedCam = 300;
            Cursor.visible = false; //Hacer que el cursor no esté activo
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void botonContinuar()
    {
        panelOpciones.SetActive(false);
        panelActivo = false;
    }

    public void botonSalir()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void botonControles()
    {
        panelControles.SetActive(true);
    }

    public void botonCerrarControles()
    {
        panelControles.SetActive(false);
    }
}
