using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion1 : MonoBehaviour
{
    public MoveState moveState = MoveState.Vibing;

    public BoidSettings boidSettings;
    public Vector3 direction = Vector3.zero;

    // for gizmos
    private Vector3 boundaryVector = Vector3.zero;
    private Vector3 avoidTargetVector = Vector3.zero;
    private Vector3 newDirection = Vector3.zero;

    public SpriteRenderer spriteRenderer;

    public enum MoveState
    {
        Seeking,
        Vibing,
        Structure,
    }

    public void Start()
    {
        moveState = MoveState.Vibing;
        direction = Random.insideUnitCircle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngle(direction)));
    }
    
    void Update()
    {
        if (moveState == MoveState.Vibing)
        {
            UpdateDirectionVibing();

            transform.position += transform.up * boidSettings.speed * Time.deltaTime;
        }
    }

    private void UpdateDirectionVibing()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, boidSettings.radius, LayerMask.GetMask("Lion")); // no view cone

        Vector3 separationVector = Vector3.zero;
        Vector3 alignmenetVector = Vector3.zero;
        Vector3 structureVector = Vector3.zero;

        foreach (Collider2D hit in hits)
        {
            Lion1 other = hit.GetComponent<Lion1>();
            if (other != null && other.moveState == MoveState.Vibing)
            {
                Vector3 dif = transform.position - other.transform.position;
                if (dif.magnitude == 0)
                    continue;
                separationVector += dif * (boidSettings.radius / dif.sqrMagnitude);
                alignmenetVector += other.direction;
                //centerVector += other.transform.position;
            }
        }
        
        Collider2D[] structureHits = Physics2D.OverlapCircleAll(transform.position, boidSettings.radius, LayerMask.GetMask("Structure")); // no view cone

        foreach (Collider2D hit in structureHits)
        {
            LionStructure other = hit.GetComponent<LionStructure>();
            if (other != null && !other.isComplete)
            {
                Vector3 dir = other.transform.position - transform.position;
                structureVector += dir;
            }
        }

        avoidTargetVector = transform.position - LionSpawner.GetInstance().target.position;
        avoidTargetVector *= boidSettings.radius / Mathf.Pow(avoidTargetVector.sqrMagnitude / 8, 4);

        float boundaryX = 0;
        float boundaryY = 0;
        // upper bound
        if (LionSpawner.GetTopVibeBound() - transform.position.y < boidSettings.radius)
        {
            boundaryY = LionSpawner.GetTopVibeBound() - transform.position.y - boidSettings.radius;
        }
        // lower bound
        else if (-LionSpawner.GetTopVibeBound() - transform.position.y > -boidSettings.radius)
        {
            boundaryY = -LionSpawner.GetTopVibeBound() - transform.position.y + boidSettings.radius;
        }

        // right bound
        if (LionSpawner.GetRightVibeBound() - transform.position.x < boidSettings.radius)
        {
            boundaryX = LionSpawner.GetRightVibeBound() - transform.position.x - boidSettings.radius;
        }
        // left bound
        else if (-LionSpawner.GetRightVibeBound() - transform.position.x > -boidSettings.radius)
        {
            boundaryX = -LionSpawner.GetRightVibeBound() - transform.position.x + boidSettings.radius;
        }

        boundaryVector = new Vector3(boundaryX, boundaryY);

        newDirection = Vector3.zero;
        newDirection += separationVector * boidSettings.separationWeight;
        newDirection += alignmenetVector * boidSettings.alignmentWeight;
        newDirection += structureVector * boidSettings.structureWeight;

        newDirection += avoidTargetVector * boidSettings.avoidTargetWeight;
        newDirection += boundaryVector * boidSettings.boundaryWeight;

        RotateTowards(newDirection);
    }

    public void StartSeeking()
    {
        StartCoroutine(SeekTarget());
    }

    private IEnumerator SeekTarget()
    {
        moveState = MoveState.Seeking;
        spriteRenderer.color = Color.red;
        Transform target = LionSpawner.GetInstance().target;
        
        // move to far enough away
        while ((transform.position - target.position).magnitude < boidSettings.distUntilAttack)
        {
            UpdateDirectionVibing();
            RotateTowards(avoidTargetVector);

            transform.position += transform.up * boidSettings.speed * Time.deltaTime;

            yield return null;
        }

        // go for a pass at the target
        Vector3 acceleration = boidSettings.acceleration * (target.position - transform.position);
        Vector3 velocity = direction * boidSettings.speed / 2;
        float minSpeed = 999999;
        float minDist = (target.position - transform.position).sqrMagnitude;
        bool staySeeking = true;
        while (staySeeking)
        {
            Vector3 dir = target.position - transform.position;
            acceleration = boidSettings.acceleration * dir;
            velocity += acceleration * Time.deltaTime;

            transform.position += velocity * Time.deltaTime;
            direction = velocity.normalized;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngle(direction)));

            minDist = Mathf.Min(minDist, dir.sqrMagnitude);
            // once you've gone far from the center
            if (dir.sqrMagnitude - minDist > 4)
            {
                minSpeed = Mathf.Min(minSpeed, velocity.sqrMagnitude);

                // and speed no longer decreases
                if (velocity.sqrMagnitude > minSpeed)
                {
                    staySeeking = false;
                }
            }
            yield return null;
        }


        moveState = MoveState.Vibing;
        spriteRenderer.color = Color.white;
    }

    private void RotateTowards(Vector3 targetDirection)
    {
        if (targetDirection.sqrMagnitude < 1)
            direction += (targetDirection - direction) * boidSettings.rotationSpeed * Time.deltaTime;
        else
            direction += (targetDirection.normalized - direction) * boidSettings.rotationSpeed * Time.deltaTime;

        direction = direction.normalized;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngle(direction)));
    }

    private float GetAngle(Vector3 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; // -90 because the sprite faces up naturally 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + direction);

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.position + newDirection);

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, transform.position + boundaryVector);

        Gizmos.color = Color.magenta;

        Gizmos.DrawLine(transform.position, transform.position + avoidTargetVector);
    }
}
