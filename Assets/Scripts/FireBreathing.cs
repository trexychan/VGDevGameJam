using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathing : MonoBehaviour
{
    
    public Transform breathStart;
    public GameObject flamePrefab;

    public float flameForce = 10f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            ShootFire();
        }
    }

    void ShootFire()
    {
        GameObject flame = Instantiate(flamePrefab, breathStart.position, breathStart.rotation);
        Rigidbody2D rb = flame.GetComponent<Rigidbody2D>();
        rb.AddForce(breathStart.up * flameForce, ForceMode2D.Impulse);
    }
}
