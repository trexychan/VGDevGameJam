using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillScore : MonoBehaviour
{
    public Text scoreTxt;

    void Update()
    {
        scoreTxt.text = LionSpawner.GetScore().ToString();
    }
}
