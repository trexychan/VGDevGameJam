using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathing : MonoBehaviour
{
    /**
     * 
     */
    public Transform breathStart;
    public float x, y, z;
    public GameObject flamePrefab;
    public float maxSpread;

    public float flameForce = 10f;
    public int i = 0;
    public float rate = 0.1f;

    void Start()
    {
        x = breathStart.rotation.eulerAngles.x;
        y = breathStart.rotation.eulerAngles.y;
        z = breathStart.rotation.eulerAngles.z;
    }

    private IEnumerator Breathe()
    {
        while (true)
        {
            
            Vector2 dir = breathStart.up + new Vector3(Random.Range(-maxSpread, maxSpread), Random.Range(-maxSpread, maxSpread), 0);
            GameObject flame = Instantiate(flamePrefab, breathStart.position, breathStart.rotation);
            Rigidbody2D rb = flame.GetComponent<Rigidbody2D>();
            rb.AddForce(dir * flameForce, ForceMode2D.Impulse);
            Debug.Log(i++);
            yield return new WaitForSeconds(rate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootFire();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine("Breathe");
        }
    }

    void ShootFire()
    {
        StartCoroutine("Breathe");
    }
}
