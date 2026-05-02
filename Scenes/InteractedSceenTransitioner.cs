using UnityEngine;

public class InteractedSceenTransitioner : MonoBehaviour
{
    public Scene sceneToChangeTo;

    public void ChangeScene()
    {
        SceneManager.instance.ChangeSceneToNewScene(sceneToChangeTo);
    }
}
