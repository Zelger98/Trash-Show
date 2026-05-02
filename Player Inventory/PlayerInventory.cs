using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    public List<PlayerInventorySlot> inventorySlots;

    public PlayerInventorySlot currentSelectedInventoryItemSlot;

    [SerializeField] GameObject itemOptionsMenu;
    [SerializeField] TextInfo unableToUseItemInfo;
    [SerializeField] AudioClip buttonSound;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        itemOptionsMenu.SetActive(false);
    }

    public bool TryAddItemToInventory(InteractableObjectCloseUpSO itemCloseUpSO)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.itemInSlot == null)
            {
                if (itemCloseUpSO.inventoryItem.takeToInventorySound != null)
                {
                    AudioManager.instance.PlaySound(itemCloseUpSO.inventoryItem.takeToInventorySound);
                }
                slot.AddItemToSlot(itemCloseUpSO);
                return true;
            }
        }
        return false;
    }

    public void ShowItemOptions(PlayerInventorySlot itemSlot)
    {
        PlayButtonSound();
        if (itemSlot.itemInSlot == null)
        {
            return;
        }
        
        if (currentSelectedInventoryItemSlot == itemSlot)
        {
            HideItemOptions();
            return;
        }

        if (currentSelectedInventoryItemSlot != null)
        {
            currentSelectedInventoryItemSlot.DeselectItemSlot();
        }

        itemOptionsMenu.SetActive(true);
        currentSelectedInventoryItemSlot = itemSlot;
        currentSelectedInventoryItemSlot.SelectItemSlot();
    }

    public void HideItemOptions()
    {
        currentSelectedInventoryItemSlot.DeselectItemSlot();

        itemOptionsMenu.SetActive(false);
        currentSelectedInventoryItemSlot = null;
    }

    public void InspectItem()
    {
        ObjectInspectionManager.instance.StartObjectInspectFromInventoryItem(currentSelectedInventoryItemSlot.itemInSlot.interactableObjectCloseUpSO);

        HideItemOptions();
    }

    public void UseSelectedItem()
    {
        PlayButtonSound();
        if (currentSelectedInventoryItemSlot.itemInSlot.TryGetComponent<IInventoryUsable>(out IInventoryUsable usable))
        {
            if (usable.TryUseItem())
            {
                RemoveCurrentSelectedItem();
            }
        }
    }

    public void UnableToUseItemWarning()
    {
        InfoTextManager.instance.ShowText(unableToUseItemInfo);
        HideItemOptions();
    }


    public void RemoveCurrentSelectedItem()
    {
        currentSelectedInventoryItemSlot.RemoveItemFromSlot();
        HideItemOptions();
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySound(buttonSound);
    }
}
