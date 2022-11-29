using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Coin : MonoBehaviour
{
    public Text display;
    int score = 0;
    public GameOverScreen gameOverScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
	        score += 1;
	        display.text = score.ToString();
        }

        if (collision.tag == "GameOver")
        {
            gameOverScreen.Setup(score);
        }
    }
}
