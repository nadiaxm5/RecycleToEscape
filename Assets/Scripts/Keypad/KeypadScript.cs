using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadScript : MonoBehaviour
{
    public MachineController machine;
    public string password;
    public int passwordLimit = 2;
    public TextMeshProUGUI passwordText;

    public AudioSource correctSound;
    public AudioSource wrongSound;

    private void Start()
    {
        passwordText.text = "";
    }

    public void PasswordEntry(string number)
    {
        if (number == "Clear")
        {
            Clear();
            return;
        }
        else if (number == "Enter")
        {
            Enter();
            return;
        }

        int length = passwordText.text.ToString().Length;
        if (length < passwordLimit)
        {
            passwordText.text = passwordText.text + number;
        }
    }

    public void Clear()
    {
        passwordText.text = "";
        passwordText.color = Color.white;
    }

    private void Enter()
    {
        if (passwordText.text == password)
        {
            correctSound.Play();

            passwordText.color = Color.green;
            StartCoroutine(CallMachine());
            StartCoroutine(WaitAndClear());
        }
        else
        {
            wrongSound.Play();

            passwordText.color = Color.red;
            StartCoroutine(WaitAndClear());
        }
    }

    IEnumerator WaitAndClear()
    {
        yield return new WaitForSeconds(0.75f);
        Clear();
    }

    IEnumerator CallMachine()
    {
        yield return new WaitForSeconds(0.75f);
        machine.StartAnimation();
    }
}
