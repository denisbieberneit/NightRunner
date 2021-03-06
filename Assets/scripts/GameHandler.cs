using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGame()
    {
        string playerLevel = PlayerPrefs.GetString("SelectedLevel");
        if (playerLevel == "" || playerLevel == null)
        {
            playerLevel = "Level1";
        }
        SceneManager.LoadScene(playerLevel);
    }
}
