using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void PlayGame()
    {
        GameStatics.GoToGameLevel();
    }

    public void OpenCredits()
    {

    }

    public void QuitGame()
    {
        GameStatics.ExitGame();
    }
}
