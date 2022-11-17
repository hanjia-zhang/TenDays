using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    // information on what the crop will grow into
    SeedData seedToGrow;

    [Header("Stages of Life")]
    public GameObject preseed;
    private GameObject seed;
    private GameObject seedling;
    private GameObject harvestable;

    // The grow points of the crop
    int growth;
    // How many growth points it takes before it becomes harvestable
    int maxGrowth;

    public enum CropState
    {
        Presseed, Seed, Seedling, Harvestable
    }

    // The current stage in the crop's growth
    public CropState cropState;

    // Initialisation for the crop GameObject
    // Called when the player plants a seed
    public void Plant(SeedData seedToGrow)
    {
        // Save the seed information
        this.seedToGrow = seedToGrow;

        // Instantiate the seed, seedling and harvestable GameObjects
        seed = Instantiate(seedToGrow.seed, transform);
        seedling = Instantiate(seedToGrow.seedling, transform);

        // Access the crop item data
        ItemData cropToYield = seedToGrow.cropToYield;

        // Instantiate the harvestable crop
        harvestable = Instantiate(cropToYield.inTheGround, transform);

        // Convert hour to Grow into minutes
        maxGrowth = GameTimestamp.HourToMinute(seedToGrow.hoursToGrow);

        // Set the initial state to seed
        // SwitchState(CropState.Presseed);

        SwitchState(CropState.Seed);

        Debug.Log("Plant function called");

    }

    // The crop will grow when watered
    public void Grow()
    {
        // Increase the growth point by 1
        growth++;

        // The seed will sprout into a seedling when the growth is at 50%
        if (growth >= maxGrowth/2 && cropState == CropState.Seed)
        {
            SwitchState(CropState.Seedling);
        }

        // Fully growth
        if(growth >= maxGrowth && cropState == CropState.Seedling)
        {
            SwitchState(CropState.Harvestable);
        }
    }

    // Function to handle the state changes
    void SwitchState(CropState stateToSwitch)
    {
        // Reset everything and set all GameObjects to inactive
        preseed.SetActive(false);
        seed.SetActive(false);
        seedling.SetActive(false);
        harvestable.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.Presseed:
                // Enable the Seed GameObject
                preseed.SetActive(true);
                break;
            case CropState.Seed:
                // Enable the Seed GameObject
                seed.SetActive(true);
                break;
            case CropState.Seedling:
                // Enable the Seedling GameObject
                seedling.SetActive(true);
                break;
            case CropState.Harvestable:
                // Enable the Harvestable GameObject
                harvestable.SetActive(true);
                // Unparent it to the crop
                harvestable.transform.parent = null;

                Destroy(gameObject);
                break;
        }

        // Set the current crop state to the state we're switching to
        cropState = stateToSwitch;
    }
}
