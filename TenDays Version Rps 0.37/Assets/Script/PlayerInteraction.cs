using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    ThirdPersonMovement playerController;

    // The raisedBed the player is currently selecting
    RaisedBed selectedLand = null;

    // The interactable object the player is currently selecting
    InteractableObject selectedInteractable = null;

    // Start is called before the first frame update
    void Start()
    {
        // Get access to the thirdpersonmovement component
        playerController = transform.parent.GetComponent<ThirdPersonMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            OnInteractableHit(hit);
        }
    }

    // Handles what happens when the interaction raycast hits something interactable
    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;
        
        // Check if the player is going to interact with land
        if (other.tag == "RaisedBed")
        {
            RaisedBed land = other.GetComponent<RaisedBed>();
            SelectedLand(land);
            return;
        }

        // Check if the player is going to interact with an item
        if (other.tag == "Item")
        {
            // Set the interactable to the currently selected interactable 
            selectedInteractable = other.GetComponent<InteractableObject>();
            return;
        }

        // Deselected the interactable if player is not stand on anything;
        if (selectedInteractable != null)
        {
            selectedInteractable = null;
        }

        // Unselect the land if the player is not choose on any land at the moment
        if (selectedLand != null)
        {
            selectedLand.Select(false);
            selectedLand = null;
        }

    }

    // Handle the selection process
    void SelectedLand(RaisedBed land) 
    {
        // Set the previously selected land to false. (If any)
        if (selectedLand != null)
        {
            selectedLand.Select(false);
        }
        // Set the new selected land to the land we are selecting now.
        selectedLand = land;
        land.Select(true);
        
    }

    // Trigger when the player presses the farming interact key
    public void Interact()
    {
        // Check if the player is selecting any land
        if (selectedLand != null)
        {
            selectedLand.Interact();
            return;
        }

        Debug.Log("Not on any land!");
    }

    // Triggered when the player presses the item interact button
    public void ItemInteract()
    {
        // if the player is holding something, keep it in his inventory
        if(InventoryManager.Instance.equippedItem != null)
        {
            InventoryManager.Instance.HandToInventory();

            return;
        }

        // if the player isn't holding anything, pick up an item


        //Check if there is an interactable selected
        if (selectedInteractable != null)
        {
            // Pick it up
            selectedInteractable.Pickup();

            Debug.Log("Picked Up!");
        }

    }
}
