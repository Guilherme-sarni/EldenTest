using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SG
{


    public class PlayerUIManager : MonoBehaviour
   
    {
        public static PlayerUIManager instance;

        [Header("Network Join")]
        [SerializeField] bool startGamesAsClient;
        public PlayerUIHUDManager playerUIHudManager;
        // Start is called before the first frame update
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            playerUIHudManager = GetComponentInChildren<PlayerUIHUDManager>();
        }
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (startGamesAsClient) 
            {
            startGamesAsClient = false;
            NetworkManager.Singleton.Shutdown();
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}