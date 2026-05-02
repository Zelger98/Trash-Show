using UnityEngine;

public class PersistantManager : MonoBehaviour
{
    public static PersistantManager instance;
    public int deathCount;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
