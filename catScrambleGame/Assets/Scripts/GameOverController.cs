using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Button playAgainButton;
    public Button mainMenuButton;
    public Button exitButton;

    void Start()
    {
        playAgainButton.onClick.AddListener(PlayAgain);
        mainMenuButton.onClick.AddListener(MainMenu);
        exitButton.onClick.AddListener(ExitGame);
    }

    void PlayAgain()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
