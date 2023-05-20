using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private MusicPlayer musicPlayer;

    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed;
    private int index;
    public int nivel;
    public AudioSource clockAlarm;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true; //Hacer que el cursor esté activo
        Cursor.lockState = CursorLockMode.None;
        musicPlayer = FindObjectOfType<MusicPlayer>();
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() //To write the characters 1 by 1
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1) //Al terminar la linea, se vacía y empieza la siguiente
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else //Cuando termina todo el texto, pasa al siguiente nivel
        {
            if(nivel == 0)
            {
                SceneManager.LoadScene("Nivel1");
            }
            if (nivel == 1)
            {
                SceneManager.LoadScene("Nivel2");
            }
            if (nivel == 2)
            {
                SceneManager.LoadScene("Nivel3");
            }
            if (nivel == 3)
            {
                musicPlayer.pauseAudio();
                clockAlarm.Play();
                StartCoroutine(BackToMainMenu());
            }
        }
    }

    IEnumerator BackToMainMenu()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainMenu");
    }

    public void ContinueDialogue()
    {
        if (dialogueText.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = lines[index];
        }
    }
}
