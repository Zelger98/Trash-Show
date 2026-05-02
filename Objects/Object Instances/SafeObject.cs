
using System.Collections.Generic;
using UnityEngine;

public class SafeObject : MonoBehaviour
{
    public static SafeObject instance;

    [SerializeField] Collider2D colliderToDisable;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openSprite;
    [SerializeField] List<GameObject> objectsInside;
    private void Awake()
    {
        instance = this;
    }

    public void OpenSafe()
    {
        spriteRenderer.sprite = openSprite;
        colliderToDisable.enabled = false;

        ObjectInspectionManager.instance.StopObjectInspect();

        foreach (GameObject obj in objectsInside)
        {
            obj.SetActive(true);
        }
    }
}
