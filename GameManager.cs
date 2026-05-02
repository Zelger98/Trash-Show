using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera mainCam;
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mainCam = Camera.main;
    }

    public Vector2 GetMouseScreenPosition()
    {
        Vector2 vector = mainCam.ScreenToWorldPoint(Input.mousePosition);
        return vector;
    }
}
