using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToOutside : MonoBehaviour, IInteractableByItem
{
    [SerializeField] AudioClip doorOpenSound;
    [SerializeField] InteractableObjectCloseUpSO gun, ammo, loadedGun;

    public void UseItemOnObject()
    {
        int items = 0;

        foreach (var item in PlayerInventory.instance.inventorySlots)
        {
            if (item.itemInSlot == null)
            {
                continue;
            }

            if (item.itemInSlot.interactableObjectCloseUpSO == gun || item.itemInSlot.interactableObjectCloseUpSO == ammo)
            {
                items++;

                if (items == 2)
                {
                    SceneManager.instance.PlayTrueEndCutscene(true);
                    return;
                }
            }
            if (item.itemInSlot.interactableObjectCloseUpSO == loadedGun)
            {
                SceneManager.instance.PlayTrueEndCutscene(true);
                return;
            }
        }

        PersistantManager.instance.deathCount++;
        AudioManager.instance.PlaySound(doorOpenSound);
        SceneManager.instance.PlayDeathByDoorOpenCutscene();
    }
}
