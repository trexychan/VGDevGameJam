using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    public Camera c;
    public Vector2 moveDir, mousePos;
    public Vector4 levelBounds = new Vector4(4f,8f,-4f,-8f); // top, right, bottom, left

    // Input control
    public float inputSmoothness = 0.01f;
    public float inputSmoothingTolerance = 0.01f;

    // Update is called once per frame
    void Update()
    {
        Vector2 newMoveDir;
        newMoveDir.x = Input.GetAxisRaw("Horizontal");
        newMoveDir.y = Input.GetAxisRaw("Vertical");
        if ((newMoveDir - moveDir).sqrMagnitude > inputSmoothingTolerance)
        {
            moveDir = Vector2.Lerp(moveDir, newMoveDir, Time.deltaTime / inputSmoothness);
        }
        else
        {
            moveDir = newMoveDir;
        }

        mousePos = c.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + moveDir * speed * Time.fixedDeltaTime;
        if (newPosition.y > levelBounds.x)
        {
            newPosition.y = levelBounds.x;
        }
        if (newPosition.x > levelBounds.y)
        {
            newPosition.x = levelBounds.y;
        }
        if (newPosition.y < levelBounds.z)
        {
            newPosition.y = levelBounds.z;
        }
        if (newPosition.x < levelBounds.w)
        {
            newPosition.x = levelBounds.w;
        }

        rb.MovePosition(newPosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

    }

}
