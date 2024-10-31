using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace soundftx
{

    public class effect_sound_loading : MonoBehaviour, ISoundDataObserver
    {
        [SerializeField]
        SoundManager sound_manager;

       [SerializeField]
        Dictionary<string, SoundData_> player_effect_sound;

        [SerializeField]
        List<AudioClip> list_player_effect_sound;



        private void Awake()
        {
            // ?????????????? sound_manager ???? ????????????????? Inspector ????
            if (sound_manager == null)
            {
                Debug.LogError("SoundManager is not assigned in the Inspector.");
            }
        }

        private void OnEnable()
        {
            if (sound_manager != null)
            {
                sound_manager.RegisterObserver(this); // ????????? Observer
            }
        }

        private void OnDisable()
        {
            if (sound_manager != null)
            {
                sound_manager.UnregisterObserver(this); // ?????????????????? Observer
            }
        }

        public void OnSoundDataChanged(SoundData_ soundData)
        {
            Debug.Log("(class effect_sound_loading) use OnSoundDataChanged(SoundData_ soundData): " + soundData);

            // Load sound effects into the dictionary
            //  LoadSoundEffects(soundData);

            Debug.Log("soundData.soundFiles.player :" + soundData.soundFiles.player);

            // LoadAudioClip(soundData.soundFiles.player);

            // AudioClip audioClip = LoadAudioClip(soundData.soundFiles.player);
            //list_player_effect_sound = LoadAudioClip(soundData.soundFiles.player);
            // ????????????????????
            AudioClip[] audioClips = Resources.LoadAll<AudioClip>(soundData.soundFiles.player);

            Debug.Log("audioClips :" + audioClips.Length);
         //   Debug.Log("audioClips :" + audioClips[1].name);

            foreach (var soundCategory_2 in audioClips)
            {
                Debug.Log("audioClips " + soundCategory_2.name);
            }

            // ?????????????? List
          //  list_player_effect_sound = new List<AudioClip>();
            list_player_effect_sound.AddRange(audioClips);

            /*
            if (audioClip !=null)
            {
                Debug.Log("audioClip is not null");
            }
            else
            {
                Debug.Log("audioClip is null");

            }

            */

            //     player_effect_sound = new Dictionary<AudioClip, SoundData_>(); // Ensure the dictionary is initialized
            // player_effect_sound[audioClip] = soundData; // Store the AudioClip with the corresponding SoundData_
        }

      

        private AudioClip LoadAudioClip(string key)
        {
            // Implement logic to load the AudioClip using the key
            // For example, using Resources.Load or another method based on your project structure
            return Resources.Load<AudioClip>("sound_ftx/" + key); // Adjust the path as necessary
        }


        /*

        public void OnSoundDataChanged(SoundData_ soundData)
        {
            Debug.Log("(class  effect_sound_loading) use OnSoundDataChanged(SoundData_ soundData):  " + soundData);

            // Loop through the sound categories and log them
            foreach (var soundCategory in soundData.soundCategories.player)
            {
                Debug.Log("soundCategory: " + soundCategory.id.ToString());
                Debug.Log("soundCategory: " + soundCategory.key.ToString());

                foreach (var soundCategory_2 in soundData.soundFiles.player)
                {
                   // foreach (var soundCategory_2 in soundData.soundCategories.player)

                }



                // Handle each sound category as needed
            }
        }


        */

        //  List<string,SoundData_> player_effect_sound ;
        ///   List<string> enermy_effect_sound;
        //    List<string> boss_effect_sound;

        /*
          -player
  -enermy
  -boss
        */

        // Start is called before the first frame update
        void Start()
    {
         //   OnSoundDataChanged(player_effect_sound.ToArray().s);



    }

    // Update is called once per frame
    void Update()
    {
        
    }


}


}
