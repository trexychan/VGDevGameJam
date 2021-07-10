using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionSpawner : MonoBehaviour
{
    public Transform target;

    public Transform vibeAnchor;
    public float vibeWidth;
    public float vibeHeight;

    private static LionSpawner _instance;

    public GameObject lion1Prefab;
    public BoidSettings boidSettings;

    void Awake()
    {
        // lazy
        _instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(lion1Prefab, Random.insideUnitCircle * 4, Quaternion.identity);
            lion1Prefab.GetComponent<Lion1>().boidSettings = boidSettings;
        }
    }

    void Update()
    {
        
    }

    public static LionSpawner GetInstance()
    {
        if (_instance == null)
        {
            Debug.LogWarning("LionSpawner does not exist!");
        }
        return _instance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 a = new Vector3(-vibeWidth / 2, -vibeHeight / 2);
        Vector3 b = new Vector3(-vibeWidth / 2, vibeHeight / 2);
        Vector3 c = new Vector3(vibeWidth / 2, vibeHeight / 2);
        Vector3 d = new Vector3(vibeWidth / 2, -vibeHeight / 2);
        if (vibeAnchor != null)
        {
            a += vibeAnchor.transform.position;
            b += vibeAnchor.transform.position;
            c += vibeAnchor.transform.position;
            d += vibeAnchor.transform.position;
        }
        Gizmos.DrawLine(a, d);
        Gizmos.DrawLine(b, a);
        Gizmos.DrawLine(c, b);
        Gizmos.DrawLine(d, c);
    }
}
