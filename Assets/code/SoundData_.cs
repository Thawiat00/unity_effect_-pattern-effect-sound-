using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace soundftx
{
    [Serializable]
    public class Boss
    {
        public int id;
        public string key;
    }

    [Serializable]
    public class Enemy
    {
        public int id;
        public string key;
    }

    [Serializable]
    public class Player
    {
        public int id;
        public string key;
    }

    [Serializable]
    public class SoundCategories
    {
        public List<Player> player;
        public List<Enemy> enemy;
        public List<Boss> boss;
    }

    [Serializable]
    public class SoundFiles
    {
        public string player;
        public string enemy;
        public string boss;
    }

    [Serializable]
    public class SoundData_  // Updated name from Root to SoundData_
    {
        public SoundCategories soundCategories;
        public SoundFiles soundFiles;
    }


    //for convset dictionary
    [System.Serializable]
    public class sound_convest
    {
        public string name;   // keep name
        public AudioClip clip;  // for keep AudioClip
                                // add another field etc  volume, loop 
    }
}