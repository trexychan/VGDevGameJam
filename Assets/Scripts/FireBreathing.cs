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
    public PlayerController player;
    public int fuel;
    public int maxFuel;

    public float flameForce = 10f;
    public float rate = 0.1f;
    public float cooldownRate = 0.3f;

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
            Debug.Log(--fuel);
            yield return new WaitForSeconds(rate);
        }
    }

    private IEnumerator CoolDown()
    {
        while (fuel <= maxFuel)
        {
            Debug.Log(fuel++);
            yield return new WaitForSeconds(cooldownRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && fuel > 0)
        {
            ShootFire();
            player.speed = 2.5f;
        }
        else if (Input.GetButtonUp("Fire1") || fuel <= 0)
        {
            StopCoroutine("Breathe");
            StartCoroutine("CoolDown");
            player.speed = 5f;
        }
    }

    void ShootFire()
    {
        StartCoroutine("Breathe");
        StopCoroutine("CoolDown");
    }
}
