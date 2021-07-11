using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionBomb : LionStructure
{
    public float fuseLength = 6;
    private float fuse;
    public float rotationAcceleration = 5;
    private float rotationSpeed = 0;

    public GameObject lionBulletPrefab;

    void Start()
    {
        fuse = fuseLength;
    }
    
    void Update()
    {
        if (isComplete)
        {
            Move();

            fuse -= Time.deltaTime;
            if (fuse < 1)
            {
                foreach (Lion1 l1 in lions)
                {
                    l1.spriteRenderer.color = Color.red;
                }
            }
            if (fuse < 0)
            {
                fuse = fuseLength;
                StartCoroutine(Shoot());
            }
        }
    }

    private void Move()
    {
        rotationSpeed += rotationAcceleration * Time.deltaTime;

        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    private IEnumerator Shoot()
    {
        int n = 6;
        int o = 180 / n;
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < n; i++)
            {
                GameObject lb = Instantiate(lionBulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 360 * i / n)));
                Destroy(lb, 6);
            }

            yield return new WaitForSeconds(0.25f);

            for (int i = 0; i < n; i++)
            {
                GameObject lb = Instantiate(lionBulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 360 * i / n + o))); 
                Destroy(lb, 6);
            }

            yield return new WaitForSeconds(0.25f);
        }

        DestroyStructure();
    }
}
