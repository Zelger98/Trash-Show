using System.Collections.Generic;
using UnityEngine;

public class GunAndAmmoInventoryItems : MonoBehaviour, IInventoryUsable
{
    [SerializeField] InteractableObjectCloseUpSO closeUpSO;
    [SerializeField] InteractableObjectCloseUpSO gunCloseUp, ammoCloseUp;
    [SerializeField] InteractableObjectCloseUpSO newItemToAdd;
    [SerializeField] TextInfo textInfo;
    [SerializeField] AudioClip loadSound;

    private List<PlayerInventorySlot> slots;

    public bool TryUseItem()
    {
        slots = new List<PlayerInventorySlot>();

        foreach (PlayerInventorySlot slot in PlayerInventory.instance.inventorySlots)
        {
            if (slot.itemInSlot != null && (slot.itemInSlot.interactableObjectCloseUpSO == gunCloseUp || slot.itemInSlot.interactableObjectCloseUpSO == ammoCloseUp))
            {
                slots.Add(slot);
            }
        }

        if (slots.Count == 2)
        {
            foreach (var slot in slots)
            {
                slot.RemoveItemFromSlot();
            }


            AudioManager.instance.PlaySound(loadSound);

            PlayerInventory.instance.HideItemOptions();
            ObjectInspectionManager.instance.StopObjectInspect();
            PlayerInventory.instance.TryAddItemToInventory(newItemToAdd);
            InfoTextManager.instance.ShowText(textInfo);
        }

        return false;
    }
}
