using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public string type;
    public Sprite icon;

    [HideInInspector]
    public bool pickedUp;
    [HideInInspector]
    public bool equipped;

    [HideInInspector]
    public GameObject toolManager;
    [HideInInspector]
    public GameObject tool;
    public bool playersTool;

    [SerializeField]
    private RaycastScript raycastScript;

    private void Start()
    {
        raycastScript = FindObjectOfType<RaycastScript>();
        toolManager = GameObject.FindWithTag("ToolManager");

        if (!playersTool)
        {
            int allTools = toolManager.transform.childCount;

            for (int i = 0; i < allTools; i++)
            {
                if(toolManager.transform.GetChild(i).gameObject.GetComponent<Item>().ID == ID)
                {
                    tool = toolManager.transform.GetChild(i).gameObject;
                }
            }
        }
    }

    private void Update()
    {
        if (equipped)
        {
            if (Input.GetKeyDown(KeyCode.X)) //Guardar con la X y avisar al player
            {
                equipped = false;
                raycastScript.currentItem = "Nada";
            }
            if(equipped == false)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ItemUsage()
    {
        //Equipar item
        if(raycastScript.currentItem == "Nada") //Equipar si no hay nada equipado
        {
            tool.SetActive(true);
            tool.GetComponent<Item>().equipped = true;
            raycastScript.currentItem = type; //Actualizar el tipo de item que tiene el player equipado
            raycastScript.currentItemID = ID; //Actualizar el ID del item que tiene el player equipado
        }
        else //Cambiar objeto equipado por el nuevo
        {
            int allTools = toolManager.transform.childCount;

            for (int i = 0; i < allTools; i++)
            {
                if (toolManager.transform.GetChild(i).gameObject.GetComponent<Item>().type == raycastScript.currentItem)
                {
                    toolManager.transform.GetChild(i).gameObject.SetActive(false);
                    toolManager.transform.GetChild(i).gameObject.GetComponent<Item>().equipped = false;
                    tool.SetActive(true);
                    tool.GetComponent<Item>().equipped = true;
                    raycastScript.currentItem = type;
                    raycastScript.currentItemID = ID;
                    break;
                }
            }
        }
    }
}
