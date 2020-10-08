using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public int start = 3;

    private TextMeshProUGUI infoTxt, counterTxt;
    private float counter = 5f;
    private float counterHide = 3f;
    public bool active, finished = false;
    
    // Start is called before the first frame update
    void Start()
    {
        counterTxt = GameObject.Find("counter").GetComponent<TextMeshProUGUI>();
        infoTxt = GameObject.Find("info").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {    
            //Debug.Log("counter: " + counter);
            if (counter >= 0)
            {
                counter -= Time.deltaTime;
                counterTxt.text = "" + counter.ToString("f0");
            }
            else 
            {
                if (counterHide >= 0)
                {
                    counterHide -= Time.deltaTime;
                    infoTxt.text = "Ya";    
                }
                else
                {
                    infoTxt.text = "";
                }
                
                counterTxt.text = "";
                finished = true;
            }
        }
    }
}
