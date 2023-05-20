using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public PlayerScript playerScript;
    [SerializeField]
    private RaycastScript raycastScript;
    [SerializeField]
    public OptionsScript options;
    [SerializeField]
    public InteractWithManual manualScript;

    public GameObject inventory;
    public GameObject slotHolder;

    public bool inventoryEnabled;
    [HideInInspector]
    public int allSlots;
    [HideInInspector]
    public GameObject[] slot;    
    
    void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        raycastScript = FindObjectOfType<RaycastScript>();
        options = FindObjectOfType<OptionsScript>();
        manualScript = FindObjectOfType<InteractWithManual>();

        allSlots = slotHolder.transform.childCount;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled; //Si está abierto lo cierro y vicebersa
        }
        if (inventoryEnabled)
        {
            inventory.SetActive(true);
            playerScript.speedCam = 0; //Que no se mueva el fondo
            Cursor.visible = true; //Hacer que el cursor esté activo
            Cursor.lockState = CursorLockMode.None;
        }
        else if(!inventoryEnabled && !options.panelActivo && !manualScript.manualActivo)
        {
            inventory.SetActive(false);
            playerScript.speedCam = 300; 
            Cursor.visible = false; //Hacer que el cursor no esté activo
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

    public void AddItem(GameObject itemObject, int itemID, string itemType, Sprite itemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;

                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().ID = itemID;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().icon = itemIcon;

                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();

                slot[i].GetComponent<Slot>().empty = false;

                return; //Evitar que item se añada en todos los slots vacíos
            }  
        }
    }

    public void DeleteItem(int itemID)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if(slot[i].GetComponent<Slot>().ID == itemID)
            {
                slot[i].GetComponent<Slot>().empty = true;
                slot[i].GetComponent<Slot>().UpdateEmptySlot();
            }
        }
    }
}
