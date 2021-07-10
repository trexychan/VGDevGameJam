using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion1 : MonoBehaviour
{
    private MoveState moveState = MoveState.Vibing;

    public BoidSettings boidSettings;
    public Vector3 direction = Vector3.zero;


    enum MoveState
    {
        Seeking,
        Vibing,
    }

    void Start()
    {
        direction = Random.insideUnitCircle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngle(direction)));
    }
    
    void Update()
    {
        UpdateDirection();

        transform.position += transform.up * boidSettings.speed * Time.deltaTime;

        if (transform.position.x > 8)
        {
            transform.position = new Vector3(-8, transform.position.y);
        }
        else if (transform.position.x < -8)
        {
            transform.position = new Vector3(8, transform.position.y);
        }

        if (transform.position.y > 4.5f)
        {
            transform.position = new Vector3(transform.position.x, -4.5f);
        }
        else if (transform.position.y < -4.5f)
        {
            transform.position = new Vector3(transform.position.x, 4.5f);
        }
    }

    private void UpdateDirection()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, boidSettings.radius, LayerMask.GetMask("Lion")); // no view cone

        Vector3 separationVector = Vector3.zero;
        Vector3 alignmenetVector = Vector3.zero;
        Vector3 centerVector = Vector3.zero;

        foreach (Collider2D hit in hits)
        {
            Lion1 other = hit.GetComponent<Lion1>();
            if (other != null)
            {
                Vector3 dif = transform.position - other.transform.position;
                if (dif.magnitude == 0)
                    continue;
                separationVector += dif * (boidSettings.radius / dif.sqrMagnitude);
                alignmenetVector += other.direction;
                centerVector += other.transform.position;
            }
        }

        Vector3 targetVector = LionSpawner.GetInstance().target.position - transform.position;
        Vector3 boundaryVector = -transform.position;


        Vector3 newDirection = Vector3.zero;
        newDirection += separationVector * boidSettings.separationWeight;
        newDirection += alignmenetVector * boidSettings.alignmentWeight;
        newDirection += centerVector * boidSettings.centerWeight;

        newDirection += targetVector * boidSettings.targetWeight;
        newDirection += boundaryVector * boidSettings.boundaryWeight;

        RotateTowards(newDirection);
    }

    private void RotateTowards(Vector3 targetDirection)
    {
        direction += (targetDirection.normalized - direction) * boidSettings.rotationSpeed * Time.deltaTime;
        direction = direction.normalized;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngle(direction)));
    }

    private float GetAngle(Vector3 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}
