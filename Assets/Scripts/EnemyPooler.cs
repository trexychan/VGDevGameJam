using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    [SerializeField]
    public float spawnTime = 3f, spawnRadius = 7f;

    public GameObject[] enemies;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject enemyPrefab;
        public int size;
    }

    #region Singleton

    public static EnemyPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    //initialize pool of enemies
    public Dictionary<string, Queue<GameObject>> enemyPool;
    //list of all enemy pools
    public List<Pool> pools;
    // Start is called before the first frame update
    void Start()
    {
        enemyPool = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.enemyPrefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            enemyPool.Add(pool.tag, objectPool);
        }
        //StartCoroutine(spawnAnEnemy());
    }
    public GameObject spawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        Vector2 spawnPos = GameObject.Find("BEAN").transform.position;
        if (!enemyPool.ContainsKey(tag))
        {
            Debug.LogWarning("pool with tag " + tag + " doesn't exist!");
            return null;
        }

        GameObject objectToSpawn = enemyPool[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        objectToSpawn.transform.rotation = rotation;

        enemyPool[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
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
