using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Scenes
{
    [RequireComponent(typeof(KeyboardManager))]
    [RequireComponent(typeof(Rigidbody))]
    
    public class PlayerNetworkController : NetworkBehaviour
    {
        public float rotationSpeed = 100;
        
        [Range(0,100), SerializeField, Tooltip("Velocidad actual del personaje")]
        public float speed = 50.0f;
        private GameManager gm;
        public TextMesh nameText;

        private KeyboardManager _keyboardManager;
        private Rigidbody playerRb;
        private bool arduinoMode = false;
        private bool _canRun = false;

        public GameObject meta;

        // These are set in OnStartServer and used in OnStartClient
        public bool canRun
        {
            get => _canRun;
            set => _canRun = value;
        }
        public int playerNo;

        [SyncVar]
        public bool isReady;

        private int unaVez=0;

        public void Start()
        {
            _keyboardManager = GetComponent<KeyboardManager>();
            playerRb = GetComponent<Rigidbody>();
            gameObject.name = "Jugador" + playerNo;
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        // This fires on server when this player object is network-ready
		//el cliente se conecta al servidor
        public override void OnStartServer()
        {
            base.OnStartServer();

            // Set SyncVar values
            // this Id is unique for every connection on the server
            playerNo = connectionToClient.connectionId;
            isReady = true;
        }

        // This fires on all clients when this player object is network-ready
		//arranca el cliente
        public override void OnStartClient()
        {
            base.OnStartClient();
            // Coloca como texto el número que le envío el servidor
            //nameText.text = string.Format("Player " + playerNo+1);
        }


        // Update is called once per frame
        void Update()
        {
			// permite el movimiento del jugador de forma independiente para cada de cada cliente, de esa forma cada cliente controla su propio jugador 
            if (!isLocalPlayer || !canRun)
                return;

            

      

            // rotate
            float horizontal = Input.GetAxis("Horizontal");
            transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

            // move
            if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Animator>().SetBool("Moverse", true);
                transform.Translate(Vector3.forward*speed*Time.deltaTime);
            }
            else
            {
                GetComponent<Animator>().SetBool("Moverse", false);
            }

            if (gm.winnerPlayer.name == this.gameObject.name)
            {
                if (unaVez == 0)
                {
                    if (isServer)
                    {
                        gm.GOToWin();
                        unaVez++;
                    }
                    if (isClient)
                    {
                        gm.GOToWin();
                        unaVez++;
                    }

                }
                // gm.win = playerNo;
            }
            if (gm.loserPlayer.name == this.gameObject.name)
            {
                if (unaVez == 0)
                {
                    if (isServer)
                    {
                        gm.GOToLose();
                        unaVez++;
                    }
                    if (isClient)
                    {
                        gm.GOToLose();
                        unaVez++;
                    }

                }
                // gm.lose = playerNo;

            }


            // Se deshabilitan el resto de movimientos
            return;
            if (Input.GetKey(KeyCode.S))
            {
                GetComponent<Animator>().SetBool("Correr", true);
                transform.Translate(Vector3.forward / 20, Camera.main.transform);
            }
            else
            {
                GetComponent<Animator>().SetBool("Correr", false);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                GetComponent<Animator>().SetBool("Saltar", true);

                transform.Translate(Vector3.forward / 50, Camera.main.transform);
            }
            else
            {
                GetComponent<Animator>().SetBool("Saltar", false);
            }

        }

    }
}
