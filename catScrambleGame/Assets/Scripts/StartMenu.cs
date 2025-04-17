using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartMenu : MonoBehaviour
{
    public GameObject mainPanel;         // Drag MainPanel here
    public GameObject optionsPanel;      // Drag OptionsPanel here
    public Dropdown musicDropdown;       // Drag MusicDropdown here
    public GameObject scoreUI;

    void Start()
    {
        // Pause the game at the beginning
        Time.timeScale = 0f;
        scoreUI.SetActive(false);
        // Set panels: main visible, options hidden
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);

        // Populate music dropdown from AudioManager
        musicDropdown.ClearOptions();
        var options = new List<string>();

        foreach (var clip in AudioManager.Instance.musicClips)
        {
            options.Add(clip.name);
        }

        musicDropdown.AddOptions(options);

        // Load and set previously selected song
        int savedMusicIndex = PlayerPrefs.GetInt("SelectedSong", 0);
        musicDropdown.value = savedMusicIndex;
        musicDropdown.RefreshShownValue();
        AudioManager.Instance.PlayMusic(savedMusicIndex);

        // Add listener
        musicDropdown.onValueChanged.AddListener(OnMusicDropdownChanged);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        mainPanel.SetActive(false);
        scoreUI.SetActive(true);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
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
