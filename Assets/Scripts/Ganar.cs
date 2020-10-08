

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ganar : MonoBehaviour
{
    public bool SeAcabo= false;    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // NDePuesto= collider.GetComponent<Plater>
            SeAcabo = true;

        }
    }
}

