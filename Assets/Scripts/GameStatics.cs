using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

// functions to move from scene to scene or to control game flow
public static class GameStatics
{
    public static void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void GoToGameLevel()
    {
        PlayerPrefs.SetInt("Score",0);
        SceneManager.LoadScene(1);
    }

    public static void GoToGameOverScreen()
    {
        PlayerPrefs.SetInt("Score",LionSpawner.GetScore());
        SceneManager.LoadScene(2);
    }

    public static void ExitGame()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
