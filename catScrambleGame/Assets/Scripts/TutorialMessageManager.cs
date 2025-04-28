using UnityEngine;
using UnityEngine.UI;

public class TutorialMessageManager : MonoBehaviour
{
    public Text tutorialMessageText;

    private void Start()
    {
        ShowTutorialMessage();
    }

    void ShowTutorialMessage()
    {
        if (tutorialMessageText != null)
        {
            tutorialMessageText.text = "Use arrow keys to move, space to jump. Dodge obstacles and collect cans for more health.";
        }
        else
        {
            Debug.LogWarning("TutorialMessageText is not assigned in the Inspector.");
        }
    }
}
