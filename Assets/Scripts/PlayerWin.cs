using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class PlayerWin : MonoBehaviour
{
    public bool llego = false;
    private GameManager gm;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Meta"))
        {
            llego = true;
            if (!gm.winnerPlayer)
            {
                gm.winnerPlayer = gameObject;
            }
            else if(!gm.loserPlayer)
            {
                gm.loserPlayer = gameObject;
            }
        }
    }
}
