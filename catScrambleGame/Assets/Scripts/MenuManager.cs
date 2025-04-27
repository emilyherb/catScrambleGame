using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public Dropdown musicDropdown;
    public Dropdown levelDropdown;

    private List<string> levelNames = new List<string> { "Tutorial", "Level 1", "Level 2", "Level 3", "Endless Mode" };

    void Start()
    {
        // Pause game while in menu
        Time.timeScale = 0f;

        // Setup Music Dropdown
        musicDropdown.ClearOptions();
        var musicOptions = new List<string>();
        foreach (var clip in AudioManager.Instance.musicClips)
        {
            musicOptions.Add(clip.name);
        }
        musicDropdown.AddOptions(musicOptions);

        int savedMusicIndex = PlayerPrefs.GetInt("SelectedSong", 0);
        musicDropdown.value = savedMusicIndex;
        musicDropdown.RefreshShownValue();
        AudioManager.Instance.PlayMusic(savedMusicIndex);

        musicDropdown.onValueChanged.AddListener(OnMusicChanged);

        // Setup Level Dropdown
        levelDropdown.ClearOptions();
        levelDropdown.AddOptions(levelNames);
    }

    public void OnPlayButton()
    {
        Time.timeScale = 1f;
        string selectedLevel = levelNames[levelDropdown.value];
        SceneManager.LoadScene(selectedLevel);
    }

    public void OnOptionsButton()
    {
        // Later you can make Options Panel here
        Debug.Log("Options button clicked!");
    }

    public void OnExitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnMusicChanged(int index)
    {
        PlayerPrefs.SetInt("SelectedSong", index);
        AudioManager.Instance.PlayMusic(index);
    }
}
