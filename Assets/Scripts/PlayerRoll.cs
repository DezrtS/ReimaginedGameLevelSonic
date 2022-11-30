using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    private Rigidbody2D rig;

    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;

    [SerializeField] private Sprite regularSonic;
    [SerializeField] private Sprite rollingSonic;

    private bool rolling;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (!rolling)
            {
                Controller.instance.disableMovement(false, false);
                GetComponent<SpriteRenderer>().sprite = rollingSonic;
                rig.freezeRotation = false;
                rolling = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rig.angularVelocity = -1440;
                if (Mathf.Abs(rig.velocity.x + acceleration) >= maxSpeed)
                {
                    rig.AddForce(new Vector2(maxSpeed - rig.velocity.x, 0), ForceMode2D.Impulse);
                }
                else
                {
                    rig.AddForce(new Vector2(acceleration, 0), ForceMode2D.Impulse);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                rig.angularVelocity = 1440;
                if (Mathf.Abs(rig.velocity.x - acceleration) >= maxSpeed)
                {
                    rig.AddForce(new Vector2(-maxSpeed - rig.velocity.x, 0), ForceMode2D.Impulse);
                }
                else
                {
                    rig.AddForce(new Vector2(-acceleration, 0), ForceMode2D.Impulse);
                }
            }
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            Controller.instance.disableMovement(true, true);
            GetComponent<SpriteRenderer>().sprite = regularSonic;
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0);
            rig.freezeRotation = true;
            rolling = false;
        }
    }
}
