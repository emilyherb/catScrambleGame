using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenuCanvas;        // Drag your Start Menu canvas here
    public GameObject optionsMenuCanvas;      // Drag your Options Menu canvas here
    public Dropdown musicDropdown;            // Drag your Dropdown UI element here

    void Start()
    {
        // Pause the game when it starts
        Time.timeScale = 0f;

        // Populate music dropdown
        musicDropdown.ClearOptions();
        var options = new List<string>();

        foreach (var clip in AudioManager.Instance.musicClips)
        {
            options.Add(clip.name); // You can customize how these names appear
        }

        musicDropdown.AddOptions(options);

        // Load saved music index
        int savedMusicIndex = PlayerPrefs.GetInt("SelectedSong", 0);
        musicDropdown.value = savedMusicIndex;
        musicDropdown.RefreshShownValue();

        // Play the saved song
        AudioManager.Instance.PlayMusic(savedMusicIndex);

        // Add dropdown listener
        musicDropdown.onValueChanged.AddListener(OnMusicDropdownChanged);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        startMenuCanvas.SetActive(false);
    }

    public void OpenOptions()
    {
        optionsMenuCanvas.SetActive(true);
        startMenuCanvas.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsMenuCanvas.SetActive(false);
        startMenuCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnMusicDropdownChanged(int index)
    {
        PlayerPrefs.SetInt("SelectedSong", index);
        AudioManager.Instance.PlayMusic(index);
    }
}
