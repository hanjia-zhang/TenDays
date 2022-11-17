using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // The item information the GameObject is supposed to represent
    public ItemData item;

    public void Pickup()
    {
        // Set the player's inventory to the item
        InventoryManager.Instance.equippedItem = item;
        // Update the changed in the scene
        InventoryManager.Instance.RenderHand();
        // Destroy this instance so as to not have multiple copies
        Destroy(gameObject);

        Debug.Log("Pick Up function in Interactable Object script");
    }
}
