using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class OurNetworkManager : NetworkManager
{
    public GameObject canvasObject;
    
    

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("On start client");
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.GOToPlay();
        //gm.GOToPreLoading();
        //canvasObject.SetActive(false);
    }

    public override void OnStopHost()
    {
        canvasObject.SetActive(true);
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.GOToHome();
    }
}
