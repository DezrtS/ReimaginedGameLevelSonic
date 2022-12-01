using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float invicibilityTimer;

    private bool invincible = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (PlayerDash.instance.IsDashing())
            {
                collision.gameObject.GetComponent<Enemy>().Kill();
            } else if (!invincible)
            {
                Coin.instance.LoseCoins();
                invincible = true;
                StartCoroutine(InvicibilityTimer());
            }
        }
    }

    private IEnumerator InvicibilityTimer()
    {
        yield return new WaitForSeconds(invicibilityTimer);
        invincible = false;
    }
}
