using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string mainSceneName;

    public void StartGame()
    {
        Debug.Log("startGame");

        UnityEngine.SceneManagement.SceneManager.LoadScene(mainSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("quit Game");

        Application.Quit();
    }
}
