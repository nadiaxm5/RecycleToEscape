using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Transform cam;
    CharacterController control;

    public float speedCam;
    public float playerSpeed;
    public float gravityForce;
    public float jumpForce;

    private float camRotation = 0f;
    private float gravityMove = 0f;

    private void Start()
    {
        cam = transform.GetChild(0).GetComponent<Transform>(); //Transform del primer hijo de player
        control = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //CAMERA
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(new Vector3(0, mouseX, 0) * speedCam * Time.deltaTime);
        camRotation -= mouseY * speedCam * Time.deltaTime;
        camRotation = Mathf.Clamp(camRotation, -90, 90); //Bloquear que rote más de lo deseado
        cam.localRotation = Quaternion.Euler(new Vector3(camRotation, 0, 0));

        //MOVEMENT
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = (transform.right * moveX + transform.forward * moveZ) * playerSpeed * Time.deltaTime; //En eje X y Z del mundo
        control.Move(movement);
        control.Move(new Vector3(0, gravityMove, 0) * Time.deltaTime);

        if (!control.isGrounded) //Si no toca el suelo, que caiga
        {
            gravityMove += gravityForce;
        }
        else //Si toca el suelo, resetea aceleración de la gravedad
        {
            gravityMove = 0f;
        }
    }
}
