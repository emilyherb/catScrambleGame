using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management
using UnityEngine.UI;  // For UI elements

public class GameOverController : MonoBehaviour
{
    // Assign buttons in the inspector
    public Button playAgainButton;
    public Button mainMenuButton;
    public Button exitButton;

    void Start()
    {
        // Add listeners to buttons
        playAgainButton.onClick.AddListener(PlayAgain);
        mainMenuButton.onClick.AddListener(MainMenu);
        exitButton.onClick.AddListener(ExitGame);
    }

    void PlayAgain()
    {
        // Assuming your main gameplay scene is called "GameScene"
        SceneManager.LoadScene("GameScene");
    }

    void MainMenu()
    {
        // Assuming your main menu scene is called "MainMenu"
        SceneManager.LoadScene("MainMenu");
    }

    void ExitGame()
    {
        // Quits the game (works only in a built game)
        Debug.Log("Exiting game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
