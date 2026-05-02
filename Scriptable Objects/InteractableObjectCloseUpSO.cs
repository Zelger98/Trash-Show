using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Interactable Object close up")]
public class InteractableObjectCloseUpSO : ScriptableObject
{
    public GameObject objectToShow;
    public bool canTakeToInventory;
    public AudioClip startInspectSound;
    public InventoryItem inventoryItem;

    public bool pauseTimerWhileInteracting;

    public string objectName;
    public TextInfo textInfo;
}
