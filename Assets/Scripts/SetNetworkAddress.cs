using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetNetworkAddress : MonoBehaviour
{
    
    public TextMeshProUGUI addressText;
    public NetworkManager networkManager;

    public void GetText()
    {
        networkManager.networkAddress = addressText.text;
    }
}
