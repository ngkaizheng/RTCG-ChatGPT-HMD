using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Method to be called when the play button is pressed
    public void BackToMainMenu()
    {
        // Load the next scene in the build index
        // You can also use SceneManager.LoadScene("SceneName") to load by scene name
        SceneManager.LoadScene("MainMenu");
    }
}
