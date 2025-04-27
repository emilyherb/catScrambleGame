using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingManager : MonoBehaviour
{
    public string lightingSceneName = "LightingScene"; // Name of your lighting scene

    void Awake()
    {
        // Check if the lighting scene is already loaded, if not, load it
        if (!SceneManager.GetSceneByName(lightingSceneName).isLoaded)
        {
            SceneManager.LoadScene(lightingSceneName, LoadSceneMode.Additive);
        }
    }

    // You can also handle scene unloading if needed (optional)
    public void UnloadLightingScene()
    {
        if (SceneManager.GetSceneByName(lightingSceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(lightingSceneName);
        }
    }
}
