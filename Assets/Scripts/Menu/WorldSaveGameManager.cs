using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SG
{


    public class WorldSaveGameManager : MonoBehaviour
    {
        private PlayerManager player;
        public static WorldSaveGameManager instance;
        [Header("World scene index")]
        [SerializeField] int worldSceneIndex = 1;

        [Header("Save Data Writer")]
        private SaveFileDataWriter saveFileDataWriter;

        [Header("Character Slots")]
        public CharacterSaveData characterSlot01;
        [Header("Current Character Data")]
        public CharacterSlot currentCharacterSlotBeingUsed;
        public CharacterSaveData currentCharacterData;
        public string saveFileName;


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
        private void DecideCharacterFileNameBasedOnCharacterSlotBeingUsed()
        {
            switch (currentCharacterSlotBeingUsed)
            {
                case CharacterSlot.CharacterSlot_01:
                    saveFileName = "characterSlot_01";
                    break;
                case CharacterSlot.CharacterSlot_02:
                    saveFileName = "characterSlot_02";
                    break;
                case CharacterSlot.CharacterSlot_03:
                    saveFileName = "characterSlot_03";
                    break;
                case CharacterSlot.CharacterSlot_04:
                    saveFileName = "characterSlot_04";
                    break;
                case CharacterSlot.CharacterSlot_05:
                    saveFileName = "characterSlot_05";
                    break;
                case CharacterSlot.CharacterSlot_06:
                    saveFileName = "characterSlot_06";
                    break;
                case CharacterSlot.CharacterSlot_07:
                    saveFileName = "characterSlot_07";
                    break;
                case CharacterSlot.CharacterSlot_08:
                    saveFileName = "characterSlot_08";
                    break;
                case CharacterSlot.CharacterSlot_09:
                    saveFileName = "characterSlot_09";
                    break;
                case CharacterSlot.CharacterSlot_10:
                    saveFileName = "characterSlot_10";
                    break;
                default:
                    break;
            }

            
        }
        public void SaveGame()
        {
            DecideCharacterFileNameBasedOnCharacterSlotBeingUsed();
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;
            player.saveGameDataToCurrentCharacterData(currentCharacterData);
            saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData);
        }

    public void CreateNewGame()
    {
            DecideCharacterFileNameBasedOnCharacterSlotBeingUsed();
        currentCharacterData = new CharacterSaveData();
    }
    public void LoadGame()
    {
            DecideCharacterFileNameBasedOnCharacterSlotBeingUsed();

            saveFileDataWriter= new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;
            currentCharacterData = saveFileDataWriter.LoadSaveFile();

            StartCoroutine(LoadWorldScene());
    }
    public IEnumerator LoadWorldScene()
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