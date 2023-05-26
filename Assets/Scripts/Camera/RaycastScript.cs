using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaycastScript : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private Lvl1Controller lvl1Script;
    [SerializeField]
    private Lvl2Controller lvl2Script;
    [SerializeField]
    private InteractWithHood carHood;
    [SerializeField]
    private InteractWithUpButton upButton;
    [SerializeField]
    private InteractWithDownButton downButton;
    [SerializeField]
    private InteractWithManual manualScript;
    [SerializeField]
    private InteractWithBattery battery;
    [SerializeField]
    private InteractWithMotorOil motorOil;
    [SerializeField]
    private InteractWithMachineDoor machineDoor;

    public int range;   //Distancia del rayo
    public GameObject cam;  //Desde donde sale el rayo
    public Image pointer;
    public Image handPointer;
    [HideInInspector]
    public string currentItem;
    public int currentItemID;

    private Transform hightlight;

    //Objetos recolectables Nivel 1
    public GameObject milk;
    public GameObject box;
    public GameObject fish;
    public GameObject bear;
    public GameObject wine;

    //Objetos recolectables Nivel 2
    public GameObject allen;
    public GameObject wrench;
    public GameObject screwdriver;

    //Objetos recolectables Nivel 3
    public Animator keypadTrituradora;
    public Animator keypadPeletizadora;
    public Animator keypadInyectora;
    public GameObject cuboPlasticos;
    public GameObject cuboTriturados;
    public GameObject cuboGranulos;

    //Otros
    public GameObject cinematic;
    public Camera camPrincipal;
    public Camera camKeypadVerde;
    public Camera camKeypadRojo;
    public Camera camKeypadAzul;
    public Canvas canvas;

    //Audios
    public AudioSource audioCorrect;
    public AudioSource audioWrong;
    public AudioSource pickItem;
    public AudioSource buttonKeypad;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        lvl1Script = FindObjectOfType<Lvl1Controller>();
        lvl2Script = FindObjectOfType<Lvl2Controller>();
        carHood = FindObjectOfType<InteractWithHood>();
        upButton = FindObjectOfType<InteractWithUpButton>();
        downButton = FindObjectOfType<InteractWithDownButton>();
        manualScript = FindObjectOfType<InteractWithManual>();
        battery = FindObjectOfType<InteractWithBattery>();
        motorOil = FindObjectOfType<InteractWithMotorOil>();
        machineDoor = FindObjectOfType<InteractWithMachineDoor>();
        currentItem = "Nada";
        camPrincipal.enabled = true;
        camKeypadVerde.enabled = false;
        camKeypadRojo.enabled = false;
        camKeypadAzul.enabled = false;
    }

    private void Update()
    {
        //Hacer que lo objetos dejen de estar iluminados al dejar de pasar por encima con el mouse
        if (hightlight != null)
        {
            if (hightlight.gameObject.GetComponent<Outline>() != null)
            {
                hightlight.gameObject.GetComponent<Outline>().enabled = false;
                hightlight = null;
            }
                
        }

        pointer.enabled = true;
        handPointer.enabled = false;
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            hightlight = hit.transform;
            if (hit.collider.CompareTag("Interactuable"))
            {
                pointer.enabled = false;
                handPointer.enabled = true;

                hightlight.gameObject.GetComponent<Outline>().enabled = true; //Iluminar objeto

                if (Input.GetMouseButtonDown(0))
                {
                    Interacting interactingScript = hit.collider.gameObject.GetComponent<Interacting>();
                    InteractWithDrawer drawerScript = hit.collider.gameObject.GetComponent<InteractWithDrawer>();
                    InteractWithWheel wheelScript = hit.collider.gameObject.GetComponent<InteractWithWheel>();
                    ButtonKeypad button = hit.collider.gameObject.GetComponent<ButtonKeypad>();
                    InteractWith(interactingScript, currentItem, currentItemID, drawerScript, wheelScript, button); //Pasarle el tipo de objeto para que haga sus acciones
                }
            }

            else if (hit.collider.CompareTag("Item"))
            {
                pointer.enabled = false;
                handPointer.enabled = true;

                hightlight.gameObject.GetComponent<Outline>().enabled = true;

                //Recolectar
                if (Input.GetMouseButtonDown(0))
                {
                    pickItem.Play();
                    GameObject itemPickedUp = hit.collider.gameObject;
                    Item item = itemPickedUp.GetComponent<Item>();
                    inventory.AddItem(itemPickedUp, item.ID,item.type,item.icon);
                    item.ItemUsage();
                }
            }

            else if (hit.collider.CompareTag("Key1"))
            {
                pointer.enabled = false;
                handPointer.enabled = true;

                hightlight.gameObject.GetComponent<Outline>().enabled = true;

                //Interactuar
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(Level1Complete());
                }
            }

            else if (hit.collider.CompareTag("Key2"))
            {
                pointer.enabled = false;
                handPointer.enabled = true;

                hightlight.gameObject.GetComponent<Outline>().enabled = true;

                //Interactuar
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(Level2Complete());
                }
            }

            else if (hit.collider.CompareTag("Key3"))
            {
                pointer.enabled = false;
                handPointer.enabled = true;

                hightlight.gameObject.GetComponent<Outline>().enabled = true;

                //Interactuar
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(Level3Complete());
                }
            }
            else hightlight = null; //Si no se está pasando por encima de ningun objeto iluminable, que no se ilumine
        }
        if (inventory.inventoryEnabled) //Desactivar los punteros al abrir el inventario
        {
            pointer.enabled = false;
            handPointer.enabled = false;
        }
    }

    private void OnDrawGizmos() //Visualizar raycast en la escena
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * range);
    }

    public void InteractWith(Interacting interactingScript, string itemType, int itemID, InteractWithDrawer drawerScript, InteractWithWheel wheelScript, ButtonKeypad button)
    {
        if (interactingScript.interactingType == "Plastic")
        {
            if (itemType != null && itemType == interactingScript.interactingType)
            {
                milk.SetActive(false); //A lo mejor añadir animación
                currentItem = "Nada";
                inventory.DeleteItem(itemID);
                audioCorrect.Play();
                lvl1Script.wasteRecycled++;
            }
            else audioWrong.Play();
        }

        if (interactingScript.interactingType == "Glass")
        {
            if (itemType != null && itemType == interactingScript.interactingType)
            {
                wine.SetActive(false); //A lo mejor añadir animación
                currentItem = "Nada";
                inventory.DeleteItem(itemID);
                audioCorrect.Play();
                lvl1Script.wasteRecycled++;
            }
            else audioWrong.Play();
        }

        if (interactingScript.interactingType == "Organic")
        {
            if (itemType != null && itemType == interactingScript.interactingType)
            {
                fish.SetActive(false); //A lo mejor añadir animación
                currentItem = "Nada";
                inventory.DeleteItem(itemID);
                audioCorrect.Play();
                lvl1Script.wasteRecycled++;
            }
            else audioWrong.Play();
        }

        if (interactingScript.interactingType == "Paper")
        {
            if (itemType != null && itemType == interactingScript.interactingType)
            {
                box.SetActive(false); //A lo mejor añadir animación
                currentItem = "Nada";
                inventory.DeleteItem(itemID);
                audioCorrect.Play();
                lvl1Script.wasteRecycled++;
            }
            else audioWrong.Play();
        }

        if (interactingScript.interactingType == "Other")
        {
            if (itemType != null && itemType == interactingScript.interactingType)
            {
                bear.SetActive(false); //A lo mejor añadir animación
                currentItem = "Nada";
                inventory.DeleteItem(itemID);
                audioCorrect.Play();
                lvl1Script.wasteRecycled++;
            }
            else audioWrong.Play();
        }

        if(interactingScript.interactingType == "Drawer")
        {
            drawerScript.DrawerInteraction();
        }

        if (interactingScript.interactingType == "CarHood")
        {
            carHood.HoodInteracion();
        }

        if (interactingScript.interactingType == "UpButton")
        {
            upButton.ButtonInteraction();
        }

        if (interactingScript.interactingType == "DownButton")
        {
            downButton.ButtonInteraction();
        }

        if (interactingScript.interactingType == "LlavePlana")
        {
            if (itemType != null && itemType == interactingScript.interactingType)
            {
                wrench.SetActive(false); //A lo mejor añadir animación
                battery.BatteryInteraction();
                currentItem = "Nada";
                inventory.DeleteItem(itemID);
                audioCorrect.Play();
                lvl2Script.tasksDone++;
            }
            else audioWrong.Play();
        }

        if (interactingScript.interactingType == "LlaveAllen")
        {
            if (itemType != null && itemType == interactingScript.interactingType)
            {
                allen.SetActive(false); //A lo mejor añadir animación
                currentItem = "Nada";
                inventory.DeleteItem(itemID);
                motorOil.MotorOilInteraction();
                audioCorrect.Play();
                lvl2Script.tasksDone++;
            }
            else audioWrong.Play();
        }

        if (interactingScript.interactingType == "Pistola")
        {
            if (itemType != null && itemType == interactingScript.interactingType)
            {
                audioCorrect.Play();
                lvl2Script.wheelsOut++;
                wheelScript.WheelInteraction(interactingScript.wheelSide);
                if (lvl2Script.wheelsOut == 5)
                {
                    screwdriver.SetActive(false); //A lo mejor añadir animación
                    currentItem = "Nada";
                    inventory.DeleteItem(itemID);
                    lvl2Script.tasksDone++;
                }
            }
            else audioWrong.Play();
        }

        if (interactingScript.interactingType == "Button")
        {
            buttonKeypad.Play();
            button.SendKey();
        }

        if (interactingScript.interactingType == "Manual")
        {
            manualScript.AbrirManual();
        }

        if (interactingScript.interactingType == "MachineDoor")
        {
            machineDoor.MachineDoorInteracion();
        }

        if (interactingScript.interactingType == "Plasticos")
        {
            if (itemType != null && itemType == interactingScript.interactingType)
            {
                cuboPlasticos.SetActive(false); //A lo mejor añadir animación
                currentItem = "Nada";
                inventory.DeleteItem(itemID);
                audioCorrect.Play();
                StartCoroutine(KeypadVerde());
            }
            else audioWrong.Play();
        }

        if (interactingScript.interactingType == "Triturados")
        {
            if (itemType != null && itemType == interactingScript.interactingType)
            {
                cuboTriturados.SetActive(false); //A lo mejor añadir animación
                currentItem = "Nada";
                inventory.DeleteItem(itemID);
                audioCorrect.Play();
                StartCoroutine(KeypadRojo());
            }
            else audioWrong.Play();
        }

        if (interactingScript.interactingType == "Granulos")
        {
            if (itemType != null && itemType == interactingScript.interactingType)
            {
                cuboGranulos.SetActive(false); //A lo mejor añadir animación
                currentItem = "Nada";
                inventory.DeleteItem(itemID);
                audioCorrect.Play();
                StartCoroutine(KeypadAzul());
            }
            else audioWrong.Play();
        }
    }

    IEnumerator Level1Complete()
    {
        cinematic.SetActive(true);
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene("DialogoNivel1");
    }

    IEnumerator Level2Complete()
    {
        cinematic.SetActive(true);
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene("DialogoNivel2");
    }

    IEnumerator Level3Complete()
    {
        cinematic.SetActive(true);
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene("DialogoNivel3");
    }

    IEnumerator KeypadVerde()
    {
        keypadTrituradora.Play("KeypadTrituradora");
        yield return new WaitForSeconds(0.1f);
        canvas.enabled = false;
        camPrincipal.enabled = false;
        camKeypadVerde.enabled = true;
        yield return new WaitForSeconds(1.5f);
        canvas.enabled = true;
        camPrincipal.enabled = true;
        camKeypadVerde.enabled = false;
    }

    IEnumerator KeypadRojo()
    {
        keypadPeletizadora.Play("KeypadPeletizadora");
        yield return new WaitForSeconds(0.1f);
        canvas.enabled = false;
        camPrincipal.enabled = false;
        camKeypadRojo.enabled = true;
        yield return new WaitForSeconds(1.5f);
        canvas.enabled = true;
        camPrincipal.enabled = true;
        camKeypadRojo.enabled = false;
    }

    IEnumerator KeypadAzul()
    {
        keypadInyectora.Play("KeypadInyectora");
        yield return new WaitForSeconds(0.1f);
        canvas.enabled = false;
        camPrincipal.enabled = false;
        camKeypadAzul.enabled = true;
        yield return new WaitForSeconds(1.5f);
        canvas.enabled = true;
        camPrincipal.enabled = true;
        camKeypadAzul.enabled = false;
    }
}
