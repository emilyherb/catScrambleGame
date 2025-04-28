using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingManager : MonoBehaviour
{
    public string lightingSceneName = "LightingScene";

    void Awake()
    {
        if (!SceneManager.GetSceneByName(lightingSceneName).isLoaded)
        {
            SceneManager.LoadScene(lightingSceneName, LoadSceneMode.Additive);
        }
    }

    public void UnloadLightingScene()
    {
        if (SceneManager.GetSceneByName(lightingSceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(lightingSceneName);
        }
    }
}
