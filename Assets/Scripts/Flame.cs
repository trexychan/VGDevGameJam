using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
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
}
