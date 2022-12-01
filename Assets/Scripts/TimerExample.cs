using UnityEngine;
using UnityEngine.UI; 

public class TimerExample : MonoBehaviour
{
    public Text display;

    float value;
   
    // Start is called before the first frame update
    void Start()
    {
        value = 0;
    }

    // Update is called once per frame
    void Update()
    {
		value += Time.deltaTime;
	    display.text = value.ToString();
    }
}
