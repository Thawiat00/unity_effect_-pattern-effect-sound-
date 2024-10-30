using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace soundftx
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private SoundData_ soundData; // Updated variable name to match new class name

        void Start()
        {
            LoadSoundEffects();
        }

        void LoadSoundEffects()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("effect_sound"); // ???????? sounds.json ??????????? Resources
            if (jsonFile != null)
            {
                soundData = JsonUtility.FromJson<SoundData_>(jsonFile.text);
                Debug.Log("Sound data loaded successfully from Resources.");
            }
            else
            {
                Debug.LogError("Sound effects file not found in Resources folder.");
            }
        }

       

        public void PlaySound(string category, string effectKey)
        {
            // Example method to play sound based on category and effectKey
        }
    }

}