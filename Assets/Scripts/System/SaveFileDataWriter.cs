using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;


namespace SG
{

    public class SaveFileDataWriter
    {
        public string saveDataDirectoryPath = "";
        public string saveFileName = "";

        public bool CheckToSeeIfFileExists()
        {
            if (File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void  DeleteSaveFile ()
        {
            File.Delete(Path.Combine(saveDataDirectoryPath,saveFileName));
        }
        public void CreateNewCharacterSaveFile(CharacterSaveData characterData)
        {
            string savePath = Path.Combine(saveDataDirectoryPath,saveFileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("Creating Save File, at Save Path: " + savePath);
                string dataStore = JsonUtility.ToJson(characterData, true);
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using(StreamWriter fileWriter = new StreamWriter(stream))
                    {
                        fileWriter.Write(dataStore);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.LogError("Error Whilst Tryng to save Character data,game not saved" + savePath + "/n" + ex);
            }
        }

        public CharacterSaveData LoadSaveFile()
        {
            CharacterSaveData characterData = null;
            string loadPath = Path.Combine(saveDataDirectoryPath, saveFileName);
            if (File.Exists(loadPath))
            {
                try
                {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream (loadPath, FileMode.Open))
                    {
                        using (StreamReader Reader = new StreamReader(stream))
                        {
                            dataToLoad = Reader.ReadToEnd();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                
            }
            return characterData;
        }

    }
}