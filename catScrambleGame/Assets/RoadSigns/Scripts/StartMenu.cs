using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenuCanvas; // Drag your StartMenuCanvas here in Inspector

    void Start()
    {
        // Pause the game when it starts
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        // Resume the game
        Time.timeScale = 1f;

        // Hide the menu UI
        startMenuCanvas.SetActive(false);
    }

    public void OpenOptions()
    {
        // You can replace this with real options UI later
        Debug.Log("Options menu opened (not implemented yet).");
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // So it quits in the editor
#endif
    }
}
