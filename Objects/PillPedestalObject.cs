using UnityEngine;

public class PillPedestalObject : MonoBehaviour, IInteractableByItem
{
    [SerializeField] GameObject pillSprite;
    [SerializeField] Collider2D interactionCollider;
    [SerializeField] FireAxeBox fireAxeBox;

    public void UseItemOnObject()
    {
        ObjectInspectionManager.instance.StopObjectInspect();
        pillSprite.SetActive(true);
        interactionCollider.enabled = false;
        fireAxeBox.OpenUp();
    }
}
