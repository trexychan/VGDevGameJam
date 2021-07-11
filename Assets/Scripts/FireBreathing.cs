using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathing : MonoBehaviour
{
    
    public Transform breathStart;
    public float x, y, z;
    public GameObject flamePrefab;
    public float maxSpread;
    public PlayerController player;
    public float fuel;
    public float fuelRate = 5;
    private float baseFuelRate;
    public float fuelAcceleration = 5;
    //public float dFuel;
    public float maxFuel;
    public float pow;

    public float flameForce = 10f;
    public float dT = 0.1f;
    public float cooldownRate = 0.3f;
    public float fireRateAcceleration = 0.002f;

    void Start()
    {
        x = breathStart.rotation.eulerAngles.x;
        y = breathStart.rotation.eulerAngles.y;
        z = breathStart.rotation.eulerAngles.z;
        baseFuelRate = fuelRate;
        StartCoroutine("CoolDown");
    }

    private IEnumerator Breathe()
    {
        while (true)
        {
            
            Vector2 dir = breathStart.up + new Vector3(Random.Range(-maxSpread, maxSpread), Random.Range(-maxSpread, maxSpread), 0);
            GameObject flame = Instantiate(flamePrefab, breathStart.position, breathStart.rotation);
            Rigidbody2D rb = flame.GetComponent<Rigidbody2D>();
            StartCoroutine(WaitToDespawnFlames(rb, flame));
            rb.AddForce(dir * flameForce, ForceMode2D.Impulse);
            //fuel -= Mathf.Pow(pow -= 0.1f, 1/2);
            //pow -= 0.1f;
            fuel -= 1;
            //if (pow <= 0)
            //{
            //    pow = 0;
            //}
            dT += fireRateAcceleration;
            //yield return new WaitForSeconds(Mathf.Pow(dT / 2, 2));
            CameraShake.Shake(0.25f, 3f * (fuel / 100));
            yield return new WaitForSeconds((Mathf.Pow(fuel - 100, 2) / 40000f)); // MAKE SURE MAX FUEL IS 100
        }
    }
    private IEnumerator CoolDown()
    {
        while (fuel <= maxFuel)
        {
            if (fuel >= maxFuel)
            {
                fuel = maxFuel;
                yield break;
            }
            else if (fuel < 0)
            {
                fuel = 0;
            }
            fuelRate += fuelAcceleration * Time.deltaTime;
            fuel += fuelRate * Time.deltaTime;
            //fuel = fuel + Mathf.Pow(pow += 0.1f, 2);
            dT -= fireRateAcceleration;
            yield return null;
        }
    }

    private IEnumerator WaitToDespawnFlames(Rigidbody2D rb, GameObject flame)
    {
        yield return new WaitForSeconds(2);
        Destroy(rb);
        Destroy(flame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && fuel > 0)
        {
            ShootFire();
            player.speed = 2.5f;
            fuelRate = baseFuelRate;
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
