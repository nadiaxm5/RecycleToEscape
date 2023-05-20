using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject panelControles;
    public GameObject panelVolumen;

    private void Start()
    {
        Cursor.visible = true; //Hacer que el cursor esté activo
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("DialogoInicio");
    }

    public void OpenPanelControles()
    {
        panelControles.SetActive(true);
    }

    public void ClosePanelControles()
    {
        panelControles.SetActive(false);
    }

    public void OpenPanelVolumen()
    {
        panelVolumen.SetActive(true);
    }

    public void ClosePanelVolumen()
    {
        panelVolumen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
