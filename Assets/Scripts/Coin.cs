using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public static Coin instance;

    public Text display;

    [SerializeField] private ParticleSystem dropCoins;

    int score = 0;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        display.text = score.ToString();
    }


    public void LoseCoins()
    {
        if (score - 7 > 0)
        {
            score = score - 7;
            dropCoins.Play();
        } else if (score == 0)
        {
            SceneManager.LoadScene("End");
        } else
        {
            dropCoins.Play();
            score = 0;
        }
        display.text = score.ToString();
    }

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
    }
}
