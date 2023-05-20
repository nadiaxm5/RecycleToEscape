using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithBattery : MonoBehaviour
{
    public GameObject cableRojo;
    public GameObject cableNegro;

    public void BatteryInteraction()
    {
        StartCoroutine("RemoveTheBattery");
    }

    private IEnumerator RemoveTheBattery()
    {
        for (float i = 0f; i <= 1.5f; i += 0.01f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
                transform.localPosition.y + 0.01f,
                transform.localPosition.z);
            cableRojo.transform.localPosition = new Vector3(transform.localPosition.x,
                cableRojo.transform.localPosition.y - 0.01f,
                cableRojo.transform.localPosition.z);
            cableNegro.transform.localPosition = new Vector3(transform.localPosition.x,
                cableNegro.transform.localPosition.y - 0.01f,
                cableNegro.transform.localPosition.z);
            yield return new WaitForSeconds(0f);
        }
        cableNegro.SetActive(false);
        cableRojo.SetActive(false);
        gameObject.SetActive(false);
    }
}
