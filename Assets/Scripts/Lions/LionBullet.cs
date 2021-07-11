using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionBullet : MonoBehaviour
{
    public float speed;
    public bool canMove = true;
    private float t;

    private void FixedUpdate()
    {
        if (canMove)
        {
            t += Time.deltaTime;
            if (t > 20)
            {
                Destroy(gameObject);
            }

            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        LionSpawner.AddLionKill(1);
    }
}
