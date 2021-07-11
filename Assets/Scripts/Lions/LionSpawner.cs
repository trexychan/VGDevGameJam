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

    private static int totalKills = 0;

    public GameObject lion1Prefab;
    public BoidSettings boidSettings;

    public GameObject lion2Prefab;
    public GameObject lion3Prefab;
    public GameObject lionStarPrefab;
    public GameObject lionGunPrefab;
    public GameObject lionBombPrefab;

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
        StartCoroutine(SetLionStructures());
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

    public static int GetScore()
    {
        return totalKills;
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
            if (l1 != null)
            {
                l = l1.gameObject;
                l.SetActive(true);
                inactiveLion1.RemoveAt(inactiveLion1.Count - 1);
                l1.animator.runtimeAnimatorController = l1.animatorControllersList[0];
                // l1.spriteRenderer.color = Color.white;
            }
            else
            {
                Debug.LogError("Destroyed Lion in the inactive pool!!!!");
                l = Instantiate(lion1Prefab);
                l1 = l.GetComponent<Lion1>();
                l1.boidSettings = boidSettings;
            }
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
        if (lion.moveState != Lion1.MoveState.Structure)
        {
            currentLionNumber -= 1;
        }
        totalKills += 1;

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

    public static void RemoveCurrentLionCount(int amount)
    {
        currentLionNumber -= amount;
    }

    public static void AddLionKill(int amount)
    {
        totalKills += amount;
    }

    private IEnumerator SetLionStructures()
    {
        while (true)
        {
            float targetNum = 10 * Mathf.Log10(currentLionNumber + 10) - 10; // this only changes on currentLionNumber change
                                                                             // also its not even right bc this is % per frame not total #
            if (Random.Range(0, 100) < targetNum && activeLion1.Count > 0)
            {
                Lion1 randomLion = activeLion1[Random.Range(0, activeLion1.Count)];
                if (randomLion.moveState == Lion1.MoveState.Vibing)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        GameObject lionN = Instantiate(lion2Prefab, randomLion.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
                        lionN.GetComponent<LionN>().AddLion(randomLion);
                    }
                    else
                    {
                        GameObject lionN = Instantiate(lion3Prefab, randomLion.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
                        lionN.GetComponent<LionN>().AddLion(randomLion);
                    }
                }
            }

            yield return new WaitForSeconds(0.09f);
        }
    }


    private IEnumerator SetLionSeekers()
    {
        while (true)
        {
            float targetNum = 3 * Mathf.Log10(currentLionNumber + 10) - 10; // this only changes on currentLionNumber change
                                                                             // also its not even right bc this is % per frame not total #
            if (Random.Range(0, 100) < targetNum && activeLion1.Count > 0)
            {
                activeLion1[Random.Range(0, activeLion1.Count)].StartSeeking();
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public static LionStructure CreateRandomEmptyLionStructure(LionN ln1)
    {
        GameObject lsObj = null;
        switch (Random.Range(0, 3))
        {
            case 0:
                lsObj = Instantiate(_instance.lionStarPrefab, ln1.transform.position, ln1.transform.rotation);
                break;
            case 1:
                lsObj = Instantiate(_instance.lionGunPrefab, ln1.transform.position, ln1.transform.rotation);
                break;
            case 2:
                lsObj = Instantiate(_instance.lionBombPrefab, ln1.transform.position, ln1.transform.rotation);
                break;
        }
        return lsObj.GetComponent<LionStructure>();
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
