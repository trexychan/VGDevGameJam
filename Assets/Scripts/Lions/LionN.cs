using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionN : LionStructure
{
    private Vector3 acceleration = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private float speed = 5;
    private float rotationSpeed = 5;
    private float drag = 0.03f;

    void Start()
    {
        StartCoroutine(UpdateAcceleration());
    }
    
    void Update()
    {
        if (isComplete)
        {
            velocity += acceleration * Time.deltaTime;
            velocity *= (1 - drag);

            //transform.position += velocity * Time.deltaTime;
            AddToPosition(velocity * speed * Time.deltaTime);
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
            //Debug.Log(acceleration + " v: " + velocity + " t: " + transform.position);
        }
    }

    private IEnumerator UpdateAcceleration()
    {
        while (true)
        {
            hitbox.enabled = false;
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 6, LayerMask.GetMask("Structure")); // no view cone
            hitbox.enabled = true;

            LionStructure ls = null;
            float minDist = 10000;
            foreach (Collider2D h in hits)
            {
                LionStructure temp = h.GetComponent<LionStructure>();
                if (temp != null && (!temp.isComplete || h.GetComponent<LionN>() != null))
                {
                    float dist = (temp.transform.position - transform.position).sqrMagnitude;
                    if (dist < minDist)
                    {
                        minDist = dist;
                        ls = temp;
                    }
                }
            }

            if (ls != null)
            {
                acceleration = 4 * (ls.transform.position - transform.position).normalized;

                rotationSpeed = Random.Range(-1, 1) * 12;

                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                acceleration = Random.insideUnitCircle * 2;
                rotationSpeed = Random.Range(-1, 1) * 8;

                yield return new WaitForSeconds(1);
            }

        }
    }

    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (isComplete && lions.Count > 0 && collision.gameObject.tag == "Structure")
        {
            LionN lN = collision.gameObject.GetComponent<LionN>();
            if (lN != null)
            {
                LionStructure ls = LionSpawner.CreateRandomEmptyLionStructure(this);

                foreach (Lion1 lion1 in lions)
                {
                    ls.AddLion(lion1);
                }

                foreach (Lion1 lion1 in lN.lions)
                {
                    ls.AddLion(lion1);
                }

                lions.Clear();
                lN.lions.Clear();
                Destroy(gameObject);
                Destroy(lN.gameObject);
            }
        }
    }
}
