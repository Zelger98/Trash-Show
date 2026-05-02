using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInventorySlot : MonoBehaviour, IPointerDownHandler
{
    public InventoryItem itemInSlot;
    public Image selectedObjectHighlight;

    public void AddItemToSlot(InteractableObjectCloseUpSO itemCloseUpSO)
    {
        InventoryItem newItem = Instantiate(itemCloseUpSO.inventoryItem, transform);
        newItem.image.SetNativeSize();
        newItem.interactableObjectCloseUpSO = itemCloseUpSO;
        itemInSlot = newItem;
    }

    public void RemoveItemFromSlot()
    {
        Destroy(itemInSlot.gameObject);
        itemInSlot = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (itemInSlot != null )
        {
            PlayerInventory.instance.ShowItemOptions(this);
        }
    }

    public void SelectItemSlot()
    {
        selectedObjectHighlight.enabled = true;
    }

    public void DeselectItemSlot()
    {
        selectedObjectHighlight.enabled = false;
    }
}
