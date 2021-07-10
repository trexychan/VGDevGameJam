using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    EnemyPooler pooler;
    private void Start()
    {
        pooler = EnemyPooler.Instance;
    }
    private void FixedUpdate()
    {
        pooler.spawnFromPool("Lion", transform.position, Quaternion.identity);
    }
}
