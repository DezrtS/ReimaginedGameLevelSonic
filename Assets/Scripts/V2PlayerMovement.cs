using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2PlayerMovement : MonoBehaviour
{
    public LayerMask ground;
    Vector2 forwardNormal = Vector2.zero;
    Rigidbody2D rig;
    private Vector3 startingPosition;
    private Vector3 pathTo = Vector3.zero;
    float acceleration = 0.1f;
    Vector2 lastVelocity = Vector2.zero;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(transform.right);
        //if (pathTo != Vector3.zero)
        //{
        //if (pathTo.x > transform.position.x)
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (Mathf.Abs(rig.velocity.x + acceleration) >= 10)
            {
                //rig.AddForce((GetMaxSpeed() - rig.velocity.x) * forwardNormal, ForceMode2D.Impulse);
                rig.velocity = Mathf.Abs(lastVelocity.x) * (Vector2)transform.right * Mathf.Sign(lastVelocity.x) + new Vector2(0, rig.velocity.y);
            }
            else
            {
                rig.velocity = Mathf.Abs(lastVelocity.x) * (Vector2)transform.right * Mathf.Sign(lastVelocity.x) + new Vector2(0, rig.velocity.y);
                rig.AddForce(acceleration * forwardNormal, ForceMode2D.Impulse);
            }
        } //else
        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            if (Mathf.Abs(rig.velocity.x - acceleration) >= 10)
            {
                //rig.AddForce((-GetMaxSpeed() - rig.velocity.x) * forwardNormal, ForceMode2D.Impulse);
                rig.velocity = Mathf.Abs(lastVelocity.x) * (Vector2)transform.right * Mathf.Sign(-lastVelocity.x) + new Vector2(0, rig.velocity.y);
            }
            else
            {
                rig.velocity = Mathf.Abs(lastVelocity.x) * (Vector2)transform.right * Mathf.Sign(-lastVelocity.x) + new Vector2(0, rig.velocity.y);
                rig.AddForce(-acceleration * forwardNormal, ForceMode2D.Impulse);
            }
        }
        //}

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1.5f, ground);
        if (hit)
        {
            rig.gravityScale = 1f;
            forwardNormal = (Vector2)(Quaternion.Euler(0, 0, -90) * hit.normal);
            transform.eulerAngles = new Vector3(0, 0, 1) * ((Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg - 90) * Mathf.Sign(90 - transform.eulerAngles.y)) + new Vector3(0, transform.eulerAngles.y, 0);
        }
        else
        {
            rig.gravityScale = 1;
            forwardNormal = Vector2.right;
            transform.rotation = Quaternion.identity;
        }
        lastVelocity = rig.velocity;
    }
}
