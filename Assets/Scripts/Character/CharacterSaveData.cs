using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [System.Serializable]
    public class CharacterSaveData
    {
        
            [Header("character Name")]
            public string characterName;
            [Header("Time Played")]
            public float secondsPlayed;


            public float xPosition;
            public float yPosition;
            public float zPosition;


        
    }
}