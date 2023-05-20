using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonKeypad : MonoBehaviour
{
    public string number;

    public void SendKey()
    {
        transform.GetComponentInParent<KeypadScript>().PasswordEntry(number);
    }
}
