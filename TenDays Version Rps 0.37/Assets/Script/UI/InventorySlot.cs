using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    ItemData ItemToDisplay;

    public Image itemDisplayImage;

    int slotIndex;
    public void Display(ItemData itemToDisplay)
    {
        // check if there is an item to display
        if(itemToDisplay != null)
        {
            // Switch the thumbnail over
            itemDisplayImage.sprite = itemToDisplay.thumbnail;
            this.ItemToDisplay = itemToDisplay;
            itemDisplayImage.gameObject.SetActive(true);

            return;
        }

        itemDisplayImage.gameObject.SetActive(false);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        // Move item from inventory to hand
        InventoryManager.Instance.InventoryToHand(slotIndex);
    }

    // Set the slot index
    public void AssignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }

    // Display the item info on the item info box when player mouses hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(ItemToDisplay);
    }

    // Reset the item info when player leaves
    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(null);
    }
}
