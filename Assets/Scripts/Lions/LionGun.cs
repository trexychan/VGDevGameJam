using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionGun : LionStructure
{
    private Transform target;
    public GameObject lionBulletPrefab;
    public Transform bulletSpawn;

    public float fireRate;
    private float timeTillShot;

    void Start()
    {
        target = LionSpawner.GetInstance().target;
        timeTillShot = fireRate;
    }

    void Update()
    {
        if (isComplete)
        {
            Move();

            timeTillShot -= Time.deltaTime;
            if (timeTillShot < 0)
            {
                timeTillShot = fireRate;
                StartCoroutine(Shoot());
            }
        }
    }

    private void Move()
    {
        Vector3 dif = target.position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg - 90);
    }

    private IEnumerator Shoot()
    {
        GameObject lb = Instantiate(lionBulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        lb.GetComponent<LionBullet>().canMove = false;

        yield return new WaitForSeconds(1);

        lb.GetComponent<LionBullet>().canMove = true;
        Destroy(lb, 5);
    }
}
