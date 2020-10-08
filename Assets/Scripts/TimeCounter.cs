using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    private GameManager gm;

    public float elapsedTime = 0f; 
    public TextMeshProUGUI timeTxt;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.subState == GameManager.gameSubStates.Run && timeTxt)
        {
            elapsedTime += Time.deltaTime;
            timeTxt.text = elapsedTime.ToString("f0") + '"';
        }
        else
        {
            timeTxt.text = "";
        }
    }
}
