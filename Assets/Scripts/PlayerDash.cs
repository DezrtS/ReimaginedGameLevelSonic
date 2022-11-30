using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashRange;
    [SerializeField] private float dashSpeed;

    private Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject closestEnemy = EnemyManager.instance.GetClosestEnemy(transform.position, 5);
            if (!closestEnemy.IsUnityNull())
            {
                rig.velocity = Vector2.zero;
                Vector2 velocity = (closestEnemy.transform.position - transform.position).normalized * dashSpeed;
                closestEnemy.GetComponent<Enemy>().Dodge(velocity);
                rig.AddForce(velocity, ForceMode2D.Impulse);
            }
        }
    }
}
