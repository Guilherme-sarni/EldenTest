using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SG
{


    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;
        [SerializeField] int worldSceneIndex = 1;
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
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    public IEnumerator LoadNewGame()
        {
            AsyncOperation loadoperation = SceneManager.LoadSceneAsync(worldSceneIndex);
            yield return null;
        }
        public int WorldIndex()
        {
            return worldSceneIndex;
        }
    }



}