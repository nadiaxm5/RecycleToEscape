using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public GameObject item;
    public int ID;
    public string type;
    public Sprite icon;
    public bool empty;
    public Transform slotIconGameObject;
    public Sprite tick;

    [SerializeField]
    private Inventory inventory;

    private void Start()
    {
        slotIconGameObject = transform.GetChild(0);
        inventory = FindObjectOfType<Inventory>();
    }

    public void UpdateSlot()
    {
        slotIconGameObject.GetComponent<Image>().sprite = icon;
    }

    public void UseItem()
    {
        item.GetComponent<Item>().ItemUsage();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!empty)
        {
            UseItem();
            inventory.inventoryEnabled = false;
        }
    }

    public void UpdateEmptySlot()
    {
        slotIconGameObject.GetComponent<Image>().sprite = tick; //Cambiar por icono de que lo ha conseguido (icono del item con tick??)
    }
}
