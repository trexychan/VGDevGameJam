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
        SceneManager.LoadScene(1);
    }

    public static void ExitGame()
    {
        if (Application.isEditor)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else
        {
            Application.Quit();
        }
    }
}
