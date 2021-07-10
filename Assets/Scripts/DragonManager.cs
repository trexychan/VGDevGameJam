using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonManager : MonoBehaviour
{
    public float health = 100;
    public float vulnerableRadius = 5; // radius in which dragon takes damage from lions
    public float damagePerLionPerFrame = 0.01f;

    public void TakeDamage(float amount) { health -= amount;}

    void Update()
    {
        TakeDamage(CheckForLions() * damagePerLionPerFrame);

        if (health <= 0)
        {
            Debug.Log("GAME OVER!: Dragon's health below or equal to 0");
        }
    }

    private int CheckForLions()
    {
        int numLions = 0;
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, vulnerableRadius, LayerMask.GetMask("Lion")); // no view cone
        foreach (Collider2D hit in hits)
        {
            Lion1 other = hit.GetComponent<Lion1>();
            if (other != null)
            {
                numLions++;
            }
        }

        return numLions;
    }
}
