using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public float pierceCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireDuration());
    }

    private IEnumerator FireDuration()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    public void RemovePierce()
    {
        pierceCount -= 1;
        if (pierceCount < 0)
        {
            Destroy(gameObject);
        }
    }
}
