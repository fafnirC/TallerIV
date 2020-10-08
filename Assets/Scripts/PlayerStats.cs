using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror.Scenes;
using TMPro;


[RequireComponent(typeof(PlayerNetworkController))]
public class PlayerStats : MonoBehaviour
{

    public int playerId;
    public float playerDistance;

    private PlayerNetworkController _playerNetworkController;
    private GameManager gm;
    public TextMeshPro distanceTxt;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _playerNetworkController = gameObject.GetComponentInParent<PlayerNetworkController>();
        playerId = _playerNetworkController.playerNo;
        //playerDistance = Vector3.Distance(gm.meta.transform.position, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //playerId = _playerNetworkController.connectionToClient.connectionId;
        playerDistance = Vector3.Distance(gm.meta.transform.position, this.transform.position);
        distanceTxt.text = playerDistance.ToString("f0") + " mts";
    }
}
