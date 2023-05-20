using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithWheel : MonoBehaviour
{

    public void WheelInteraction(string side)
    {
        StartCoroutine("RemoveTheWheel", side);
        StartCoroutine("DeleteTheWheelCollider");
    }

    private IEnumerator RemoveTheWheel(string side)
    {
        if(side == "Left")
        {
            for (float i = 0f; i <= 1.5f; i += 0.01f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x - 0.01f,
                    transform.localPosition.y,
                    transform.localPosition.z);
                yield return new WaitForSeconds(0f);
            }
            gameObject.SetActive(false);
        }
        if (side == "Right")
        {
            for (float i = 1.5f; i >= 0f; i -= 0.01f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x + 0.01f,
                    transform.localPosition.y,
                    transform.localPosition.z);
                yield return new WaitForSeconds(0f);
            }
            gameObject.SetActive(false);
        }
        if (side == "Down")
        {
            for (float i = 1.5f; i >= 0f; i -= 0.01f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x,
                    transform.localPosition.y - 0.01f,
                    transform.localPosition.z);
                yield return new WaitForSeconds(0f);
            }
            gameObject.SetActive(false);
        }
    }

    private IEnumerator DeleteTheWheelCollider()
    {
        transform.GetComponentInParent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(0f);
    }
}
