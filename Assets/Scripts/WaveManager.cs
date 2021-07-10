using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public List<Wave> waves;

}

public class WaveManager : MonoBehaviour
{
    private enum WaveState
    {
        IDLE,
        ACTIVE
    }
    private WaveState state = WaveState.IDLE;
    void Start()
    {
        StartWave();
    }

    private void StartWave()
    {
        Debug.Log("Wave Started!");
        state = WaveState.ACTIVE;
    }

    
}
