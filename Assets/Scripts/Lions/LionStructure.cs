using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionStructure : MonoBehaviour
{
    public bool isComplete = false;
    public int lionsNeeded = 2;
    public int currentLions = 0;

    public List<Transform> lionPositionsRemaining = new List<Transform>();

    private List<Lion1> lions = new List<Lion1>();
    public Collider2D hitbox;


    protected void SetPosition(Vector3 pos)
    {
        AddToPosition(pos - transform.position);
    }

    protected void AddToPosition(Vector3 pos)
    {
        transform.position += pos;
        foreach (Lion1 l1 in lions)
        {
            l1.transform.position += pos;
        }
    }

    private void RemoveLion(Lion1 lion)
    {
        lions.Remove(lion);
        LionSpawner.RemoveLion1(lion);
        currentLions -= 1;

        if (!isComplete)
        {
            lionsNeeded += 1;
            lionPositionsRemaining.Add(lion.transform);
        }
        else
        {
            if (1.0 * currentLions / lionsNeeded < 0.5f)
            {
                DestroyStructure();
            }
        }
        
    }

    public void AddLion(Lion1 lion)
    {
        if (lions.Contains(lion))
        {
            return;
        }

        currentLions += 1;

        if (lionsNeeded < 4)
        {
            if (isComplete)
            {
                // extra lion
                LionSpawner.RemoveLion1(lion);
                return;
            }
            if (currentLions >= lionsNeeded)
            {
                isComplete = true;
            }
        }
        else if (lionsNeeded >= lionsNeeded - 2)
        {
            isComplete = true;
        }

        lions.Add(lion);

        lion.moveState = Lion1.MoveState.Structure;
        int ind = Random.Range(0, lionPositionsRemaining.Count);
        Transform t = lionPositionsRemaining[ind];
        lionPositionsRemaining.RemoveAt(ind);
        lion.transform.position = t.position;
        lion.transform.rotation = t.rotation;
        lion.spriteRenderer.color = Color.blue;
    }

    private void DestroyStructure()
    {
        foreach (Lion1 l1 in lions)
        {
            LionSpawner.RemoveLion1(l1);
        }

        Destroy(gameObject);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Collided!");
    //    if (!isComplete && collision.gameObject.layer == LayerMask.GetMask("Lion"))
    //    {
    //        Lion1 l1 = collision.gameObject.GetComponent<Lion1>();
    //        if (l1 != null && l1.moveState == Lion1.MoveState.Vibing)
    //        {
    //            AddLion(l1);
    //        }
    //    }
    //    if (!isComplete && collision.gameObject.layer == LayerMask.GetMask("Structure"))
    //    {
    //        LionN lN = collision.gameObject.GetComponent<LionN>();
    //        if (lN != null)
    //        {
    //            foreach (Lion1 lion1 in lN.lions)
    //            {
    //                AddLion(lion1);
    //            }

    //            Destroy(lN.gameObject);
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided!");
        if (!isComplete && collision.gameObject.tag == "Lion")
        {
            Lion1 l1 = collision.gameObject.GetComponent<Lion1>();
            Debug.Log("l1: " + l1);
            if (l1 != null && l1.moveState == Lion1.MoveState.Vibing)
            {
                AddLion(l1);
            }
        }
        if (!isComplete && collision.gameObject.tag == "Structure")
        {
            LionN lN = collision.gameObject.GetComponent<LionN>();
            if (lN != null)
            {
                foreach (Lion1 lion1 in lN.lions)
                {
                    AddLion(lion1);
                }

                Destroy(lN.gameObject);
            }
        }
    }
}
