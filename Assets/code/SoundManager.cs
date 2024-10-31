using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using soundftx;
using UnityEngine;

namespace soundftx
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private SoundData_ soundData; // Updated variable name to match new class name

      //  [SerializeReference]
        private List<ISoundDataObserver> observers = new List<ISoundDataObserver>();


        public void RegisterObserver(ISoundDataObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void UnregisterObserver(ISoundDataObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        private void NotifyObserversAfterLoad(SoundData_ soundData)
        {
            Debug.Log(" NotifyObserversAfterLoad(SoundData_ soundData) working");

            foreach (var observer in observers)
            {
                Debug.Log("  foreach (var observer in observers)");
                observer.OnSoundDataChanged(soundData);
            }
        }

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

                NotifyObserversAfterLoad(soundData);

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

public interface ISoundDataObserver
{
    void OnSoundDataChanged(SoundData_ soundData);
}