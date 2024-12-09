using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace SG
{

    public class SaveFileDataWriter : MonoBehaviour
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


    }
}