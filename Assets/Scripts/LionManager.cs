using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionManager : MonoBehaviour
{
    public int health;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Deplete health until 0 then despawn
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Flame flame = collider.GetComponent<Flame>();
    }


}
