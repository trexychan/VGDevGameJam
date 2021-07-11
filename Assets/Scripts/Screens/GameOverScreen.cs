using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text scoreText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        score = LionSpawner.GetScore();
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
