using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionUI : MonoBehaviour
{
    public void SampleLevel()
    {
        //tells unit to load the main game scenes.
        SceneManager.LoadScene("SampleLevel");
    }

    public void ReturnToMenu()
    {
        //closes the game
        SceneManager.LoadScene("MainMenu");
    }
}
