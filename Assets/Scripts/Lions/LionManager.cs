using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionManager : MonoBehaviour
{
    public int health;
    public SpriteRenderer spriteRenderer;
    public bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive && health <= 0)
        {
            alive = false;
            LionSpawner.RemoveLion1(GetComponent<Lion1>());
        }
    }

    // Deplete health until 0 then despawn
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Flame flame = collider.GetComponent<Flame>();
        if (alive && flame != null)
        {
            Debug.Log("LION HIT BY FIRE");
            StartCoroutine(BurnLion());
        }
    }

    private IEnumerator BurnLion()
    {
        yield return new WaitForSeconds(2f);
        TakeDamage(1000);
        Debug.Log("KILL LION");
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
}
