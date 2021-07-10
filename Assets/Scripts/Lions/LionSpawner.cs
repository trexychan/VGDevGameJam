using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionSpawner : MonoBehaviour
{
    public Transform target;

    public Transform vibeAnchor;
    public float vibeWidth;
    public float vibeHeight;

    public float targetLionNumber = 10;
    private static float currentLionNumber = 0;
    private static List<Lion1> activeLion1 = new List<Lion1>();
    private static List<Lion1> inactiveLion1 = new List<Lion1>();


    public GameObject lion1Prefab;
    public BoidSettings boidSettings;

    private static LionSpawner _instance;

    void Awake()
    {
        // lazy
        _instance = this;
    }

    private void Start()
    {
        //for (int i = 0; i < 30; i++)
        //{
        //    GameObject l = Instantiate(lion1Prefab, Random.insideUnitCircle * 4, Quaternion.identity);
        //    activeLion1.Add(l.GetComponent<Lion1>());
        //    activeLion1[i].boidSettings = boidSettings;
        //}

        StartCoroutine(SetLionSeekers());
    }

    void Update()
    {
        targetLionNumber += Time.deltaTime / 5;

        if (currentLionNumber < targetLionNumber)
        {
            StartCoroutine(SpawnLion1());
        }
        
    }

    public static LionSpawner GetInstance()
    {
        if (_instance == null)
        {
            Debug.LogWarning("LionSpawner does not exist!");
        }
        return _instance;
    }

    public static float GetTopVibeBound()
    {
        return _instance.vibeAnchor.transform.position.y + (_instance.vibeHeight / 2);
    }

    public static float GetRightVibeBound()
    {
        return _instance.vibeAnchor.transform.position.x + (_instance.vibeWidth / 2);
    }


    private IEnumerator SpawnLion1()
    {
        currentLionNumber += 1;

        yield return new WaitForSeconds(Random.Range(0.25f, 1));

        GameObject l;
        Lion1 l1;
        if (inactiveLion1.Count > 0)
        {
            l1 = inactiveLion1[inactiveLion1.Count - 1];
            l = l1.gameObject;
            l.SetActive(true);
            inactiveLion1.RemoveAt(inactiveLion1.Count - 1);
        }
        else
        {
            l = Instantiate(lion1Prefab);
            l1 = l.GetComponent<Lion1>();
            l1.boidSettings = boidSettings;
        }

        float angle = Random.Range(0, 2 * Mathf.PI);
        Vector3 pos = 10 * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
        l.transform.position = pos;
        l1.Start();

        activeLion1.Add(l1);
    }

    public static void RemoveLion1(Lion1 lion)
    {
        currentLionNumber -= 1;

        if (activeLion1.Contains(lion))
        {
            activeLion1.Remove(lion);
        }
        else
        {
            Debug.LogWarning("Couldnt remove Lion1 from active pool!");
        }

        lion.gameObject.SetActive(false);
        inactiveLion1.Add(lion);
    }

    private IEnumerator SetLionSeekers()
    {
        while (true)
        {
            float targetNum = 10 * Mathf.Log10(currentLionNumber + 10) - 10; // this only changes on currentLionNumber change
                                                                             // also its not even right bc this is % per frame not total #
            if (Random.Range(0, 100) < targetNum && activeLion1.Count > 0)
            {
                activeLion1[Random.Range(0, activeLion1.Count)].StartSeeking();
            }

            yield return new WaitForSeconds(0.1f);
        }
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
