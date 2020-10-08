using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Mirror.Scenes
{
    public class JugadorManager : MonoBehaviour
    {
       
		//numero de jugadores para que arranque el juego
        public int MinimumPlayersForGame = 1;
        public PlayerNetworkController LocalPlayerController;
        public GameObject localPlayer;
        

		//variable que determina si arranca el juego
        public bool IsGameReady;

		//se crea una lista donde almacenará los jugadores
        public List<PlayerNetworkController> players = new List<PlayerNetworkController>();

        private void Start()
        {
	        //throw new NotImplementedException();
        }

        void GOToOnePlayer()
        {
	        MinimumPlayersForGame = 0;
        }
        
        void GOToMultiplayerPlayer()
        {
	        MinimumPlayersForGame = 2;
        }

        void Update()
        {
            //genera una instancia, True if the server or client is started and running
            if (NetworkManager.singleton.isNetworkActive && !LocalPlayerController)
            {
				//se pregunta si los jugadores estan listos
                GameReadyCheck();
				//crea a cada jugador asociandole las características del script jugador
				try
				{
					LocalPlayerController = ClientScene.localPlayer.GetComponent<PlayerNetworkController>();
				}
				catch (Exception e)
				{
				}
               
                
            }
			//si el cliente o servidor no esta corriendo 
            else
            {
                //Cleanup state once network goes offline
                IsGameReady = false;
                LocalPlayerController = null;
                players.Clear();
            }
        }
      
      

        void GameReadyCheck()
        {
            //si alguno de los jugadores NO esta listo (variable isReady de algun jugador == false)
            if (IsGameReady == false)
            {
                //Look for connections that are not in the player list
				//busca en las (kvp) conecciones y a medida que encuantra una, adiciona al cliente como un nuevo jugador en la lista players 
                foreach (KeyValuePair<uint, NetworkIdentity> kvp in NetworkIdentity.spawned)
                {
					//conexión del jugador
					PlayerNetworkController comp = kvp.Value.GetComponent<PlayerNetworkController>();
					//si el jugador y su conexión existen 
                    if (comp != null)
                    {
						//si este jugador no esta dentro de la lista de jugadores
                        if (!players.Contains(comp))
                        {
							//adicione el jugador conectado
                            players.Add(comp);
                        }
                    }
                }

                //If minimum connections has been check if they are all ready
                //si el numero de jugadores en la lista players es mayor o igual a 1
				if (players.Count >= MinimumPlayersForGame)
				{
					//se crea e inicializa la variable AllReady
					bool AllReady = true;

					//se busca en la lista de players la variable isReady de cada jugador
					foreach (PlayerNetworkController usuario in players)
					{
						//si la variable isReady del jugador falsa (ver script jugador variable is ready)
						if (usuario.isReady == false)
						{
							//entonces AllReady es falso
							AllReady = false;
						}
					}

					//si todos los jugadores estan listos (variable isReady de cada jugador == true)
					if (AllReady == true)
					{
						
						IsGameReady = true;
                        //genera que salga de la condicion, linea 41
                    }
                }
            }
        }

        
    }
}
