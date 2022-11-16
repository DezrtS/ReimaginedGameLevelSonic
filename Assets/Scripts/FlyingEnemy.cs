using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    private Rigidbody2D rig;
    private Vector3 startingPosition;
    private Vector3 pathTo = Vector3.zero;

    void Start()
    {
        SetupEnemy(true);
        rig = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        StartCoroutine(Wander());
    }

    void Update()
    {
        if (pathTo != Vector3.zero)
        {
            Vector2 nextForce = (pathTo - transform.position).normalized * GetSpeed();
            float nextSpeed = (nextForce + rig.velocity).magnitude;
            if (nextSpeed < GetMaxSpeed())
            {
                rig.AddForce(nextForce);
            } else
            {
                rig.AddForce(nextForce.normalized * (GetMaxSpeed() - nextSpeed));
            }
        }
    }

    public void Dodge(Vector2 velocity)
    {
        if (Random.Range(0, 2) == 1)
        {
            velocity.x = -velocity.x;
        } else
        {
            velocity.y = -velocity.y;
        }

        float x = velocity.x;
        velocity.x = velocity.y;
        velocity.y = x;

        rig.velocity = Vector2.zero;
        rig.AddForce(velocity.normalized * GetMaxSpeed(), ForceMode2D.Impulse);
        pathTo = (Vector2)transform.position + velocity.normalized * (GetRange());
    }

    private IEnumerator Wander()
    {
        yield return new WaitForSeconds(GetWanderDelay());
        if (Random.Range(0, 2) == 1)
        {
            Vector2 direction = new Vector2(Random.Range(-100, 101), Random.Range(-100, 101)).normalized;
            float distance = Random.Range(0, GetRange());
            pathTo = (Vector2)startingPosition + direction * distance;
        }
        StartCoroutine(Wander());
    }
}
