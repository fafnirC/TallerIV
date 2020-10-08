using System;
using System.Collections;
using System.Collections.Generic;
using Mirror.Scenes;
using Mirror;
using UnityEngine;

public class FollowMainPlayer : MonoBehaviour
{
    public Vector3 offset = new Vector3(0,4,-5);

    // Update is called once per frame
    void Update()
    {

        if (ClientScene.localPlayer)
        {
            this.transform.position = ClientScene.localPlayer.transform.position + offset;    
        }
        
    }
}