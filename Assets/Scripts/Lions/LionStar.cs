using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionStar : LionStructure
{
    private Vector3 direction;
    private float rotateSpeed;
    public float speed = 5;

    bool wasComplete = false;

    void Start()
    {
        direction = Random.insideUnitCircle;
        rotateSpeed = Random.Range(1f, 2f) * 100;
        if (Random.Range(0, 2) == 0)
            rotateSpeed *= -1;
    }
    
    void Update()
    {
        if (isComplete)
        {
            Move();
            if (!wasComplete)
                SetLions();
        }
        wasComplete = isComplete;
    }

    private void SetLions()
    {
        float n = lions.Count;
        for (int i = 0; i < n; i++)
        {
            lions[i].transform.localPosition = new Vector3(Mathf.Cos(2 * Mathf.PI * i / n) * 0.5f, Mathf.Sin(2 * Mathf.PI * i / n) * 0.5f);
            lions[i].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, (360 * i / n) - 90));
        }
    }

    private void Move()
    {
        AddToPosition(direction * speed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));

        if (transform.position.x > LionSpawner.GetRightVibeBound())
        {
            direction = new Vector3(-direction.x, direction.y);
            transform.position = new Vector3(LionSpawner.GetRightVibeBound(), transform.position.y);
        }
        else if (transform.position.x < -LionSpawner.GetRightVibeBound())
        {
            direction = new Vector3(-direction.x, direction.y);
            transform.position = new Vector3(-LionSpawner.GetRightVibeBound(), transform.position.y);
        }

        if (transform.position.y > LionSpawner.GetTopVibeBound())
        {
            direction = new Vector3(direction.x, -direction.y);
            transform.position = new Vector3(transform.position.x, LionSpawner.GetTopVibeBound());
        }
        else if (transform.position.y < -LionSpawner.GetTopVibeBound())
        {
            direction = new Vector3(direction.x, -direction.y);
            transform.position = new Vector3(transform.position.x, -LionSpawner.GetTopVibeBound());
        }
    }
}
