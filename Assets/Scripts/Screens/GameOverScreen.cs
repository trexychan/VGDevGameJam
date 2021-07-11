using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        Setup(0);
    }

    public void Setup(int score)
    {
        print("Setup");
        gameObject.SetActive(true);
        //score = LionSpawner.GetScore();
        score = PlayerPrefs.GetInt("Score",0);
        scoreText.text = "Score: " + score.ToString();
    }
    public void RestartButton()
    {
        GameStatics.GoToGameLevel();
    }
    public void MenuButton()
    {
        GameStatics.GoToMainMenu();
    }
}
