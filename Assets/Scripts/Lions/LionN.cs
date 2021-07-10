using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionN : LionStructure
{
    private Vector3 acceleration = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private float speed = 5;
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
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.z + Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg * Time.deltaTime));
            Debug.Log(acceleration + " v: " + velocity + " t: " + transform.position);
        }
    }

    private IEnumerator UpdateAcceleration()
    {
        while (true)
        {
            hitbox.enabled = false;
            Collider2D hit = Physics2D.OverlapCircle(transform.position, 6, LayerMask.GetMask("Structure")); // no view cone
            hitbox.enabled = true;

            if (hit != null)
            {
                acceleration = 4 * (hit.transform.position - transform.position).normalized;

                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                acceleration = Random.insideUnitCircle * 2;

                yield return new WaitForSeconds(1);
            }

        }
    }
}
