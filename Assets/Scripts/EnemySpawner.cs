using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public float spawnTime = 3f, spawnRadius = 7f;

    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnAnEnemy());
    }

    IEnumerator spawnAnEnemy()
    {
        Vector2 spawnPos = GameObject.Find("BEAN").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(spawnAnEnemy());
    }
}
