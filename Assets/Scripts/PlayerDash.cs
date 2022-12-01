using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public static PlayerDash instance;

    [SerializeField] private Sprite regularSonic;
    [SerializeField] private Sprite dashingSonic;

    [SerializeField] private float dashRange;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;

    private bool dashing = false;

    private Rigidbody2D rig;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Controller.instance.IsGrounded() && !dashing)
        {
            GameObject closestEnemy = EnemyManager.instance.GetClosestEnemy(transform.position, 5);
            if (!closestEnemy.IsUnityNull())
            {
                dashing = true;
                rig.velocity = Vector2.zero;
                Vector2 velocity = (closestEnemy.transform.position - transform.position).normalized * dashSpeed;
                closestEnemy.GetComponent<Enemy>().Dodge(velocity);
                rig.AddForce(velocity, ForceMode2D.Impulse);
                GetComponent<SpriteRenderer>().sprite = dashingSonic;
                StartCoroutine(DashCooldown());
            }
        } else if (dashing && Controller.instance.IsGrounded())
        {
            StopAllCoroutines();
            GetComponent<SpriteRenderer>().sprite = regularSonic;
            dashing = false;
        }
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashDuration);
        GetComponent<SpriteRenderer>().sprite = regularSonic;
        dashing = false;
    }

    public bool IsDashing()
    {
        return dashing;
    }
}
