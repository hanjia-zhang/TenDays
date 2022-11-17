using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class InventoryManager : MonoBehaviour
{
    public bool bagEnabled;
    public bool Camera;
    public GameObject bag;
    
    public UIManager n;
    public static InventoryManager Instance { get; private set;}

    public CinemachineFreeLook freeLook;

    [SerializeField] private ThirdPersonMovement tpsController;
    
    private void Awake()
    {
        // if there is more than one instance, destroy the extra.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } 
        else
        {
            //set the static instance to this instance
            Instance = this;
        }
    }

    // Item Slots
    public ItemData[] items = new ItemData[8];
    // Item in the player's hand
    public ItemData equippedItem = null;

    // The transform for the player to hold items in the scene
    public Transform handPoint;


    // Equipping

    // Handles movement of item from Inventory to Hand
    public void InventoryToHand(int slotIndex)
    {
        // Cache the Inventory slot ItemData from InventoryManager
        ItemData itemToEquip = InventoryManager.Instance.items[slotIndex];

        // Change the Inventory slot to the hand's
        items[slotIndex] = equippedItem;

        // Change the Hand's Slot to the Inventory Slot's
        equippedItem = itemToEquip;

        // Update the changes in the scene
        RenderHand();

        // Update the changes to the UI
        UIManager.Instance.RenderInventory();


    }

    // Handles movement of item from hand to inventory
    public void HandToInventory()
    {
        //Iterate through each Inventory slot and find an empty slot
        for (int i =0; i < items.Length; i++)
        {
            if(items[i] == null)
            {
                // Send the equipped item over
                items[i] = equippedItem;
                // Remove the item from the hand
                equippedItem = null;
                break;
            }
        }

        RenderHand();

        //
        UIManager.Instance.RenderInventory();
        
    }

    // Render the player's equipped item in the scene
    public void RenderHand()
    {
        // Reset objects to the hand
        if (handPoint.childCount > 0)
        {
            Destroy(handPoint.GetChild(0).gameObject);
        }


        // Check if the player has anything equipped
        if(equippedItem != null)
        {
            // Instantiate the game model on the player's hand and put it on the scene
            Instantiate(equippedItem.gameModel, handPoint);
        }
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bagEnabled = !bagEnabled;
        }
        if (bagEnabled == true)
        {
            
            bag.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            // GameObject.Find("Manager").GetComponent<UIManager>().RenderInventory();
            UIManager.Instance.RenderInventory();
            tpsController.enabled = false;

            Camera = false;
            GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = Camera;
            
        }
        else
        {
            
            bag.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            tpsController.enabled = true;

            Camera = true;
            GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = Camera;
        }
    }
}
