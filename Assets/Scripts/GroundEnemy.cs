using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundEnemy : Enemy
{

    private Rigidbody2D rig;

    private Vector3 startingPosition;
    private Vector3 pathTo = Vector3.zero;

    private float acceleration = 0.1f;

    private bool chasing = false;
    private bool dodging = false;
    private bool wandering = true;

    private GameObject chaseObject;

    private void Start()
    {
        SetupEnemy(false);
        rig = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
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
            if (pathTo.x > transform.position.x)
            {
                if (Mathf.Abs(pathTo.x - transform.position.x) > 0.2f)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                if (Mathf.Abs(rig.velocity.x + acceleration) >= GetMaxSpeed())
                {
                    rig.AddForce(new Vector2(GetMaxSpeed() - rig.velocity.x, 0), ForceMode2D.Impulse);
                }
                else
                {
                    rig.AddForce(new Vector2(acceleration, 0), ForceMode2D.Impulse);
                }
            }
            else
            {
                if (Mathf.Abs(pathTo.x - transform.position.x) > 0.2f)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                if (Mathf.Abs(rig.velocity.x - acceleration) >= GetMaxSpeed())
                {
                    rig.AddForce(new Vector2(-GetMaxSpeed() - rig.velocity.x, 0), ForceMode2D.Impulse);
                }
                else
                {
                    rig.AddForce(new Vector2(-acceleration, 0), ForceMode2D.Impulse);
                }
            }
        }
    }

    override
    public void Dodge(Vector2 velocity)
    {
        if (!dodging)
        {
            dodging = true;
            int direction;
            if (velocity.x > 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            rig.velocity = Vector2.zero;
            rig.AddForce(new Vector2(direction * GetMaxSpeed(), 0), ForceMode2D.Impulse);
            PathTo((Vector2)transform.position + new Vector2(GetMaxSpeed() / 2 * direction, 0));
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
                PathTo(new Vector2(startingPosition.x + Random.Range(-GetRange(), GetRange()), 0));
            }
            StartCoroutine(Wander());
        }
    }

    public void PathTo(Vector2 position)
    {
        pathTo = position;
        if (pathTo.x - transform.position.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
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
