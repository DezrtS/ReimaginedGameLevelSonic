using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class FlyingEnemy : Enemy
{
    private Rigidbody2D rig;

    private Vector3 startingPosition;
    private Vector3 pathTo = Vector3.zero;

    private float power;

    private bool chasing = false;
    private bool dodging = false;
    private bool wandering = true;

    private GameObject chaseObject;

    void Start()
    {
        SetupEnemy(true);
        rig = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        power = 1;
        StartCoroutine(Wander());
    }

    void Update()
    {
        if (chasing && !dodging)
        {
            PathTo(chaseObject.transform.position);
        }

        if (pathTo != Vector3.zero)
        {
            if ((pathTo - transform.position).magnitude > 0.1f)
            {
                float xVelocity;
                float yVelocity;
                float rotationSpeed = 1;
                float angle = Mathf.Atan2(rig.velocity.x, rig.velocity.y);
                if (angle > Mathf.PI)
                {
                    angle -= 2 * Mathf.PI;
                }
                else if (angle < -Mathf.PI)
                {
                    angle += 2 * Mathf.PI;
                }
                float angleToTarget = Mathf.Atan2(pathTo.y - transform.position.y, pathTo.x - transform.position.x);
                float relativeAngleToTarget = angleToTarget - angle;
                if (relativeAngleToTarget > Mathf.PI)
                {
                    relativeAngleToTarget -= 2 * Mathf.PI;
                }
                else if (relativeAngleToTarget < -Mathf.PI)
                {
                    relativeAngleToTarget += 2 * Mathf.PI;
                }
                angle += relativeAngleToTarget * rotationSpeed;
                xVelocity = Mathf.Cos(angle);
                yVelocity = Mathf.Sin(angle);
                xVelocity = xVelocity * GetMaxSpeed();
                yVelocity = yVelocity * GetMaxSpeed();
                rig.AddForce(new Vector2((xVelocity - rig.velocity.x) / (100), (yVelocity - rig.velocity.y) / (100)) * power, ForceMode2D.Impulse);
            } else
            {
                power = Mathf.Lerp(1, 0, Time.deltaTime);
            }
        }
    }

    override
    public void Dodge(Vector2 velocity)
    {
        if (!dodging)
        {
            dodging = true;
            if (Random.Range(0, 2) == 1)
            {
                velocity.x = -velocity.x;
            }
            else
            {
                velocity.y = -velocity.y;
            }

            float x = velocity.x;
            velocity.x = velocity.y;
            velocity.y = x;

            rig.velocity = Vector2.zero;
            rig.AddForce(velocity.normalized * GetMaxSpeed(), ForceMode2D.Impulse);
            PathTo((Vector2)transform.position + velocity.normalized * (GetRange()));
            StartCoroutine(DodgeTimer());
        }
    }

    private IEnumerator DodgeTimer()
    {
        yield return new WaitForSeconds(2);
        dodging = false;
    }

    private IEnumerator Wander()
    {
        yield return new WaitForSeconds(GetWanderDelay());
        if (wandering)
        {
            if (Random.Range(0, 2) == 1)
            {
                Vector2 direction = new Vector2(Random.Range(-100, 101), Random.Range(-100, 101)).normalized;
                float distance = Random.Range(0, GetRange());
                PathTo((Vector2)startingPosition + direction * distance);
            }
            StartCoroutine(Wander());
        }
    }

    public void PathTo(Vector2 position)
    {
        power = 1;
        pathTo = position;
        if (pathTo.x - transform.position.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        } else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dodging = false;
            chaseObject = collision.gameObject;
            chasing = true;
            wandering = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            chaseObject = null;
            chasing = false;
            wandering = true;
            StartCoroutine(Wander());
        }
    }
}
