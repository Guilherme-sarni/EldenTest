using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEditor.UI;

namespace SG
{
    public class TitleScreenManager : MonoBehaviour
    {
        [SerializeField] GameObject titleScreemMainMenu;
        [SerializeField] GameObject titleScreemLoadMenu;


        [Header("buttons")]
        [SerializeField] Button loadMenuReturButton;
        [SerializeField] Button mainMenuloadButton;

        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewgame()
        {
            WorldSaveGameManager.instance.CreateNewGame();
            StartCoroutine(WorldSaveGameManager.instance.LoadWorldScene());
        }
        public void OpenLoadGameMenu()
        {
            titleScreemMainMenu.SetActive(false);
            titleScreemLoadMenu.SetActive(true);
            loadMenuReturButton.select();
        }
        public void closeLoadGameMenu()
        {
            titleScreemMainMenu.SetActive(false);
            titleScreemLoadMenu.SetActive(true);
            loadMenuReturButton.Select();
        }
    }

}
