using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;
using Mirror.Scenes;
using TMPro;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    //public static GameManager gm;
    
    
    public enum gameStates {GameLoaded, Home, ConnectPlayer, SelectWorld, Playing, GameOver , Loading, Results};
    public gameStates state = gameStates.Home;
    
    public enum gameSubStates {Start, Prepare, Run, Finish };
    public gameSubStates subState = gameSubStates.Start;
    
    public GameObject NetworkManager;
    public GameObject homeCanvas, connectMultiplayerCanvas, gameOverCanvas, selectWorldCanvas, gameLoadCanvas, loadAppCanvas, winCanvas, gameCanvas;

    public GameObject gameUI, gameWaiting;

    public bool playersAreReady = false;
    public bool onGame = false;
    public float contador = 0f;
    
    public GameObject meta;
    
    public JugadorManager _jugadorManager;

    public PlayerNetworkController _PlayerController;

    private Countdown _countdown;

    public GameObject winnerPlayer;
    public GameObject loserPlayer;

    public int win;
    public int lose;

    // Start is called before the first frame update
    void Start()
    {
        _jugadorManager = GameObject.Find("JugadorManager").GetComponent<JugadorManager>();
        _countdown = gameUI.GetComponent<Countdown>();
        
        state = gameStates.GameLoaded;
    }

    // Update is called once per frame
    void Update() {
        switch (state)
        {
            case gameStates.GameLoaded:
                loadAppCanvas.SetActive(true);
                NetworkManager.SetActive(false);

                contador += Time.deltaTime;
                //Debug.Log(contador);
                if (contador >= 3f)
                {
                    hideCanvas();
                    state = gameStates.Home;
                    contador = 0f;
                }

                // nothing
                break;
            case gameStates.Home:
                NetworkManager.SetActive(false);
                homeCanvas.SetActive(true);
                break;
            case gameStates.Playing:
                gameCanvas.SetActive(true);
                checkSubState();
           
                break;
            case gameStates.GameOver:
                gameOverCanvas.SetActive(true);
                break;
            case gameStates.ConnectPlayer:
                connectMultiplayerCanvas.SetActive(true);
                NetworkManager.SetActive(true);
                // nothing
                break;
            case gameStates.SelectWorld:
                selectWorldCanvas.SetActive(true);
                break;

            case gameStates.Loading:
                gameLoadCanvas.SetActive(true);
                NetworkManager.SetActive(false);

                contador += Time.deltaTime;
                //Debug.Log(contador);
                if (contador >= 3f)
                {
                    hideCanvas();
                    GOToPlay();
                    contador = 0f;
                }
                // nothing
                break;

            case gameStates.Results:
                
                winCanvas.SetActive(true);
                // nothing
                break;
        }
    }

    void checkSubState()
    {

        switch (subState)
        {
            case gameSubStates.Start:
                gameUI.SetActive(false);
                gameWaiting.SetActive(true);
                break;
            case gameSubStates.Prepare:
                gameWaiting.SetActive(false);
                gameUI.SetActive(true);
                if (_countdown.finished)
                {
                    subState = gameSubStates.Run;
                }
                break;
            case gameSubStates.Run:
                gameWaiting.SetActive(false);
                break;
        }
        
        // Los dos jugadores estan listos para jugar
        if (_jugadorManager.IsGameReady && subState == gameSubStates.Start)
        {
            setLocalPlayer();
            subState = gameSubStates.Prepare;
            _countdown.active = true;
        }
        
        if (subState == gameSubStates.Run && _PlayerController)
        {
            _PlayerController.canRun = true;
        }
        else if (_PlayerController) _PlayerController.canRun = false;
        
    }

    void setLocalPlayer()
    {
        if (!_PlayerController)
        {
            _PlayerController = _jugadorManager.LocalPlayerController.GetComponent<PlayerNetworkController>();
        }
    }

    void hideCanvas()
    {
        homeCanvas.SetActive(false);
        connectMultiplayerCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        selectWorldCanvas.SetActive(false);
        gameLoadCanvas.SetActive(false);
        loadAppCanvas.SetActive(false);
        winCanvas.SetActive(false);
        gameCanvas.SetActive(false);
    }

    public void GOToHome()
    {
        Debug.Log("GOToHome");
        hideCanvas();
        state = gameStates.Home;
    }

    public void GOToConnectPlayer()
    {
        Debug.Log("GOToConnectPlayer");
        hideCanvas();
        state = gameStates.ConnectPlayer;
    }

    public void GOToPreLoading()
    {
        Debug.Log("GOToLoad");
        hideCanvas();
        state = gameStates.Loading;
    }

    public void GOToGameOver()
    {
        Debug.Log("GOToGameOver");
        hideCanvas();
        state = gameStates.GameOver;
    }

    public void GOToWin()
    {
        Debug.Log("GOToWin");
  
        winCanvas.SetActive(true);

        //state = gameStates.Results;
    }

    public void GOToLose()
    {
        Debug.Log("GOToLose");
        hideCanvas();
        //state = gameStates.Results;
        gameOverCanvas.SetActive(true);
    }

    public void GOToSelect()
    {
        Debug.Log("GOToSelect");
        hideCanvas();
        state = gameStates.SelectWorld;
    }
    
    public void GOToPlay()
    {
        Debug.Log("GOToPlay");
        hideCanvas();
        state = gameStates.Playing;
        subState = gameSubStates.Start;
    }

}

