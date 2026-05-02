using UnityEngine;

public class BreakableBottle : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    public void BreakBottle()
    {
        AudioManager.instance.PlaySound(breakSound);
        Destroy(gameObject);
    }
}
