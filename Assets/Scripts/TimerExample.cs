using UnityEngine;
using UnityEngine.UI; 

public class TimerExample : MonoBehaviour
{
    public Text display;

    float value;

    bool str;
   
    // Start is called before the first frame update
    void Start()
    {
        value = 0;
	str = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(str = true)
	{
		value += Time.deltaTime;
	}
	
	display.text = value.ToString();
    }
}
