using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionStructure : MonoBehaviour
{
    public bool isComplete = false;
    public int lionsNeeded = 2;
    public int currentLions = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Lion"))
        {
            Lion1 l1 = collision.gameObject.GetComponent<Lion1>();
        }
    }
}
