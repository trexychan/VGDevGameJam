using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{
    public Transform pauseMenuRoot;
    public GameObject HUD;

    public Button resumeButton;
    public Button quitButton;
    public Slider volumeSlider;

    private bool pauseMenuOpen = false;

    void Start()
    {
        SetPauseMenuActive(false);

        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitToMainMenu);
        volumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);

        // start master volume at right setting
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeSlider.value = masterVolume;
    }

    void Update()
    {
        //print("Update");
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Pause) || Input.GetKeyDown(KeyCode.P))
        {
            print("pause");
            SetPauseMenuActive(!pauseMenuOpen);
        }
    }

    private void SetPauseMenuActive(bool active)
    {
        pauseMenuOpen = active;

        HUD.SetActive(!active);

        for (int i = 0; i < pauseMenuRoot.childCount; ++i)
        {
            pauseMenuRoot.GetChild(i).gameObject.SetActive(active);
        }

        if (active)
        {
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ResumeGame()
    {
        print("resume game");
        SetPauseMenuActive(false);
    }

    public void QuitToMainMenu()
    {
        print("quit to main menu");
        GameStatics.GoToMainMenu();
    }

    public void OnMasterVolumeChanged(float newVolume)
    {
        print("on master volume changed to " + newVolume.ToString());
        AudioManager.GetInstance().ChangeVolume(newVolume);
    }
}
