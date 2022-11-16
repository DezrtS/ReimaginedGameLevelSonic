using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Coin : MonoBehaviour
{
    public Text display;
    int score = 0;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // if (collision.gameObject.tag == "Coin")
        //{
            Destroy(this.gameObject);
	    score = score + 1;
	    display.text = score.ToString();
        //}
    }
}
