using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.Assertions.Must;
using static UnityEngine.Tilemaps.TilemapRenderer;

namespace soundftx
{

    public class effect_sound_loading : MonoBehaviour, ISoundDataObserver
    {
        [SerializeField]
        bool forloading;

        [SerializeField]
        bool for_dictionary;


        [SerializeField]
        SoundManager sound_manager;


        //dicionary convest effect sound
        [SerializeField]
        Dictionary<string, AudioClip> player_effect_sound;


        //use sort data only
        [SerializeField]
        List<AudioClip> list_sort_audio;


        //for play effect with string
        [SerializeField]
        List<string> list_player_effect_sound;

        [SerializeField]
        AudioSource audioSource;

        [SerializeField]
        string status_effect;
        
    public enum status_enum_effect
        {
            idle,walking,run
        }

        [SerializeField]
         status_enum_effect Enum_effect;

        /*
    -player
-enermy
-boss
  */

        // Start is called before the first frame update
        void Start()
        {
            //   OnSoundDataChanged(player_effect_sound.ToArray().s);

            Enum_effect = status_enum_effect.idle;

            audioSource = GetComponent<AudioSource>();

        }

        // Update is called once per frame
        void Update()
        {

            To_Be_Status();




        }

        void To_Be_Status()
        {
            if(Input.anyKey == false )
            {
                //Enum_effect = status_enum_effect.idle;
                Enum_effect = status_enum_effect.idle;


                status_effect = "Idle";
                Debug.Log(" idle status" + status_effect);

               //status_enum_effect = status_enum_effect.idle;
               // return;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("Key W pressed");
                status_effect = "walking";
                Enum_effect = status_enum_effect.walking;


                if (Input.GetKey(KeyCode.Space))
                {
                    Debug.Log("Key left + w pressed");
                    status_effect = "Run";
                    Enum_effect = status_enum_effect.run;

                }
            }
        
        }

        private void Awake()
        {
            // ?????????????? sound_manager ???? ????????????????? Inspector ????
            if (sound_manager == null)
            {
                if(forloading == true)
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
            if (forloading == true)
                Debug.Log("(class effect_sound_loading) use OnSoundDataChanged(SoundData_ soundData): " + soundData);

            // Load sound effects into the dictionary
            //  LoadSoundEffects(soundData);

            if (forloading == true)
                Debug.Log("soundData.soundFiles.player :" + soundData.soundFiles.player);


            AudioClip[] audioClips = Resources.LoadAll<AudioClip>(soundData.soundFiles.player);


            // List<AudioClip> list_sort_audio = new List<AudioClip>();


            //sort loop
            // list_sort_audio.AddRange(audioClips);


            list_sort_audio.AddRange(audioClips);
            //    string clip1;


            // ?????? sort
            
            list_sort_audio.Sort((clip1, clip2) =>
            {
                // ?????????????????? string
                string clip1Name = clip1.name;
                string clip2Name = clip2.name;

                // if sort_data_sound_effect_player have data

                foreach (var sort_data_sound_effect_player in soundData.soundCategories.player)
                {
                    foreach(var audioClips1 in list_sort_audio)
                    {

                        int comparison1 = clip1Name.CompareTo(audioClips1.name.ToString());


                        //  int comparison1 = clip1Name.CompareTo(sort_data_sound_effect_player.id);
                        int comparison2 = clip2Name.CompareTo(sort_data_sound_effect_player.id.ToString());

                        // ????????????????????????
                        if (comparison1 != 0)
                        {
                            return comparison1; // ??????????????????????????
                        }
                        if (comparison2 != 0)
                        {
                            return comparison2; // ??????????????????????????
                        }

                    }
                
                }

                // ?????? 0 ?????????????????
                return 0;
            });
            
            


            player_effect_sound = new Dictionary<string, AudioClip>();


            /*
            foreach (var load_clip in audioClips)
            {
                if(for_dictionary == true)
                Debug.Log("load_clip :" + load_clip);

               
                player_effect_sound.Add(load_clip.name, load_clip);

      

                    foreach (var dictionary_player_effect_sound in player_effect_sound)
                {
                    if (for_dictionary == true)
                        Debug.Log("dictionary_player_effect_sound (name)" + dictionary_player_effect_sound.Key);

                    if (for_dictionary == true)
                        Debug.Log("dictionary_player_effect_sound (file)" + dictionary_player_effect_sound.Value);
                }

            }

            */

            foreach (var load_clip in list_sort_audio)
            {

                if (for_dictionary == true)
                    Debug.Log("load_clip :" + load_clip);

                //add in dictionary
                player_effect_sound.Add(load_clip.name, load_clip);

                //add for list string
                list_player_effect_sound.Add(load_clip.name);

                foreach (var dictionary_player_effect_sound in player_effect_sound)
                {
                    if (for_dictionary == true)
                        Debug.Log("dictionary_player_effect_sound (name)" + dictionary_player_effect_sound.Key);

                    if (for_dictionary == true)
                        Debug.Log("dictionary_player_effect_sound (file)" + dictionary_player_effect_sound.Value);
                }

            }
               




        }



        void PlayEffectSound(string effectName)
        {
           // Debug.Log(" audioSource.isPlaying :" + audioSource.isPlaying);

            // ??????????????????????????????????? list ???????
            if (list_player_effect_sound.Contains(effectName))
            {
                // ??????????????????????????? Dictionary ????????? AudioClip ?????
                if (player_effect_sound.TryGetValue(effectName, out AudioClip clip) && audioSource.isPlaying == false)
                {

                    // ????????????? AudioSource
                    // audioSource.PlayOneShot(clip);
                    // audioSource.pl
                    // audioSource.

                    audioSource.clip = clip;

                    audioSource.Play();
                }
                else
                {
                    Debug.LogWarning("music effect is playing or "+ $"Sound effect '{effectName}' not found in player_effect_sound dictionary.");
                }
            }
            else
            {
                Debug.LogWarning($"Effect '{effectName}' is not listed in list_player_effect_sound.");
            }
        }

        // player_effect_sound.AddRange(player_effect_sound);

        //  Debug.Log("player_effect_sound.AddRange(player_effect_sound) :" + player_effect_sound.Count);

        //  player_effect_sound = Resources.LoadAll<AudioClip>(soundData.soundFiles.player);

    }
}

       



  






