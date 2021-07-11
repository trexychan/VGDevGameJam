using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreen : MonoBehaviour
{
    bool creditsOpen = false;

    void Start()
    {
        CloseCreditsScreen();
    }

    void Update()
    {
        if (creditsOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCreditsScreen();
        }
    }

    public void OpenCreditsScreen()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        creditsOpen = true;
    }

    public void CloseCreditsScreen()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        
        creditsOpen = false;
    }
}
