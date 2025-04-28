using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StartMenu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public Dropdown musicDropdown;
    public Dropdown levelDropdown;
    public GameObject scoreUI;

    private List<string> levelNames = new List<string>
    {
        "Tutorial",
        "Level 1",
        "Level 2",
        "Level 3",
        "Endless Mode"
    };

    void Start()
    {
        Time.timeScale = 0f;
        scoreUI.SetActive(false);

        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);

        SetupMusicDropdown();

        SetupLevelDropdown();
    }

    private void SetupMusicDropdown()
    {
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

        musicDropdown.onValueChanged.AddListener(OnMusicDropdownChanged);
    }

    private void SetupLevelDropdown()
    {
        levelDropdown.ClearOptions();
        levelDropdown.AddOptions(levelNames);

        int savedLevelIndex = PlayerPrefs.GetInt("SelectedLevel", 0);
        levelDropdown.value = savedLevelIndex;
        levelDropdown.RefreshShownValue();

        levelDropdown.onValueChanged.AddListener(OnLevelDropdownChanged);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        mainPanel.SetActive(false);
        scoreUI.SetActive(true);

        string selectedLevelName = levelNames[levelDropdown.value];
        SceneManager.LoadScene(selectedLevelName);
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

    private void OnLevelDropdownChanged(int index)
    {
        PlayerPrefs.SetInt("SelectedLevel", index);
    }
}
