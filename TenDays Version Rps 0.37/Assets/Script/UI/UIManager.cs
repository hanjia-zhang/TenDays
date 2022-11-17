using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour, ITimeTracker
{
    public static UIManager Instance { get; private set; }

    [Header("Status Bar")]
    // Tool equip slot on the status bar
    public Image itemEquipSlot;
    // Time UI
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dayText;

    [Header("Inventory System")]
    // The inventory panel
    public GameObject InventoryPanel;

    //The item equipped slot on UI panel
    public HandInventorySlot itemHandSlot;

    // The item slot UIs
    public InventorySlot[] itemSlots;

    //Item Info Box
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

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

    public void Start()
    {
        RenderInventory(); 
        AssignSlotIndex();

        // Add UIManager to the list of objects TimeManager will notify when the time update
        TimeManager.Instance.RegisterTracker(this);
    }

    // Iterate through the slot UI elements and assign it its reference slot index
    public void AssignSlotIndex()
    {
        for (int i=0; i<itemSlots.Length; i++)
        {
            itemSlots[i].AssignIndex(i);

        }
    }
    
    // Render the inventory screen to reflect the Player's Inventory.
    public void RenderInventory()
    {
        // Get the inventory item slots from Inventory Manager
        ItemData[] inventoryItemSlots = InventoryManager.Instance.items;
        
        //Render the Item section
        RenderInventoryPanel(inventoryItemSlots, itemSlots);

        // Render the equipped slots
        itemHandSlot.Display(InventoryManager.Instance.equippedItem);

        // Get item equip from InventoryManager
        ItemData equippedItem = InventoryManager.Instance.equippedItem;

        // check if there is an item to display
        if(equippedItem != null)
        {
            // Switch the thumbnail over
            itemEquipSlot.sprite = equippedItem.thumbnail;
            itemEquipSlot.gameObject.SetActive(true);

            return;
        }

        itemEquipSlot.gameObject.SetActive(false);
    }

    // Iterate through a slot in a section and display them in the UI.
    void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        for (int i=0; i<uiSlots.Length;i++)
        {
            //Display them accordingly
            uiSlots[i].Display(slots[i]);
        }
    }

    // Display Item Info on the Item InfoBox
    public void DisplayItemInfo(ItemData data)
    {
        // If data is null, reset
        if (data == null)
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";

            return;
        }
        itemNameText.text = data.name;
        itemDescriptionText.text = data.description;
    }

    // Callback to the handle the UI for the time
    public void ClockUpdate(GameTimestamp timestamp)
    {
        // Handle the time

        // Get the hours and minutes
        int hours = timestamp.hour;
        int minutes = timestamp.minute;

        // AM or PM
        string prefix = "AM ";

        // Convert hours to 12 hour clock
        if (hours > 12)
        {
            // Time becomes PM
            prefix = "PM ";
            hours -= 12;
        }

        // Format for the time text display
        timeText.text = prefix + hours + ":" + minutes.ToString("00");

        // Handle the day
        int day = timestamp.day;
        
        // Format for the date text display
        dayText.text = "Day "+day;

    }

}
