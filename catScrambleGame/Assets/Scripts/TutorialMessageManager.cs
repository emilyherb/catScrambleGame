using UnityEngine;
using UnityEngine.UI; // For UI Text

public class TutorialMessageManager : MonoBehaviour
{
    public Text tutorialMessageText; // Reference to the UI Text

    private void Start()
    {
        // Show the tutorial message when the scene starts
        ShowTutorialMessage();
    }

    // Method to display the tutorial message
    void ShowTutorialMessage()
    {
        if (tutorialMessageText != null)
        {
            // Set the message text
            tutorialMessageText.text = "Use arrow keys to move, space to jump. Dodge obstacles and collect cans for more health.";
        }
        else
        {
            Debug.LogWarning("TutorialMessageText is not assigned in the Inspector.");
        }
    }
}
