using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public Text display;
    int score = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
	        score ++;
	        display.text = score.ToString();
            print(score);
            SoundManager.PlaySound("Coin");
        }

	    if (score == 0)
	    {
	    	SceneManager.LoadScene("End");
	    }
    }
}
