using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisedBed : MonoBehaviour, ITimeTracker
{
    public enum LandStatus
    {
        Farmland, Watered
    }

    public LandStatus landStatus;
    public Material farmlandMat, wateredMat;
    public int WaterHours = 20;
    new Renderer renderer;

    // The selection gameObject to enable when the player is selecting the land
    public GameObject select;

    // Cache the time the land was watered
    GameTimestamp timeWatered; 

    [Header("Crops")]
    // The crop prefab to instantiate
    public GameObject cropPrefab;

    // the crop currently planted on the land
    CropBehavior cropPlanted = null;

    // Start is called before the first frame update
    void Start()
    {
        // Get the renderer component
        renderer = GetComponent<Renderer>();

        // Set the land to soil by default
        SwitchLandStatus(LandStatus.Farmland);

        // Deselect the land by default
        Select(false);

        // Add this to TimeManager's Listener list
        TimeManager.Instance.RegisterTracker(this);
        
    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        // Set land status accordingly
        landStatus = statusToSwitch;
        Material materialToSwitch = farmlandMat;

        // Decide what material to switch to
        switch(statusToSwitch)
        {
            case LandStatus.Farmland:
                // Switch to the farmland material
                materialToSwitch = farmlandMat;
                break;
            case LandStatus.Watered:
                // Switch to the watered material
                materialToSwitch = wateredMat;
                // Cache the time it was watered
                timeWatered = TimeManager.Instance.GetGameTimestamp();

                break;
        }

        // Get the renderer to apply the changes
        Material[] mats = renderer.materials;
        mats[1] = materialToSwitch;
        renderer.materials = mats;
        Debug.Log(landStatus);
    }

    public void Select(bool toggle)
    {
        select.SetActive(toggle);

    }

    // When the player press the interact button while selecting this land
    public void Interact()
    {
        // Check the player's tool slot
        ItemData itemSlot = InventoryManager.Instance.equippedItem;

        // If there's nothing equipped, return
        if (itemSlot == null)
        {
            return;
        }

        // Try casting the itemdata in the itemslot as EquipmentData
        EquipmentData equipmentItem = itemSlot as EquipmentData;

        // Check if it is of type EquipmentData
        if (equipmentItem != null)
        {
            // Get the tool type
            EquipmentData.ToolType toolType = equipmentItem.toolType;

            switch(toolType)
            {
                case EquipmentData.ToolType.WaterCan:
                    SwitchLandStatus(LandStatus.Watered);
                    break;
            }

            // We don't need to check for the seeds if we have already confirmed the tool to be an equipment
            return;
        }

        // Try casting the itemdata in the itemslot as SeedData
        SeedData seedTool = itemSlot as SeedData;

        // Conditions for the player to be able to plant a seed
        if (seedTool != null && cropPlanted == null)
        {
            //Instantiate the crop object parented to the land
            GameObject cropObject = Instantiate(cropPrefab, transform);
            //Move the crop object to the top of the land gameobject

            
            cropObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.22f, transform.position.z);
            //cropObject.transform.SetParent(gameObject.transform, true);

            // Access the CropBehavior of our newly planted crop
            cropPlanted = cropObject.GetComponent<CropBehavior>();

            //Plant it
            cropPlanted.Plant(seedTool);
            Debug.Log("Has Planted, do the interact function!");

        }


        
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        // Checked if 24 hours has passed since last watered
        if(landStatus == LandStatus.Watered)
        {
            // Hours since the land was watered
            int hoursElapsed = GameTimestamp.CompareTimestamps(timeWatered, timestamp);
            Debug.Log(hoursElapsed);

            // Grow the planted crop
            if(cropPlanted != null)
            {
                cropPlanted.Grow();
            }

            if(hoursElapsed >= WaterHours)
            {
                // Dry up the land
                SwitchLandStatus(LandStatus.Farmland);
            }
        }
    }
}
