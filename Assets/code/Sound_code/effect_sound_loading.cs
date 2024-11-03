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

        [SerializeField]
        private status_enum_effect previousEffect; // ????????????????????????



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

            //oldState = (int)Enum_effect;


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
               // oldState = (int)Enum_effect;



                status_effect = "Idle";
                Debug.Log(" idle status" + status_effect);

                audioSource.Stop();
                audioSource.clip = null;

                //update old status;
                previousEffect = Enum_effect;

                //status_enum_effect = status_enum_effect.idle;
                // return;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    //if input  KeyCode.LeftShift
                    //and if previousEffect status not  status(run) , maybe will display run status
                    // ?????????????????????? run ?????????????????????
                    if (previousEffect != status_enum_effect.run)
                    {
                        Debug.Log("Key left + w pressed (run)");
                        status_effect = "run";
                        Enum_effect = status_enum_effect.run;
                        PlayEffectSound(status_effect);
                    }

                    //update old status;
                    previousEffect = Enum_effect;
                }
                else
                {
                    //if input  KeyCode.W
                    //and if previousEffect status not  status(walking) , besure will display walking status
                    // ?????????????????????? walking ?????????????????????

                    if (previousEffect != status_enum_effect.walking)
                    {
                        Debug.Log("Key W pressed (walking)");
                        status_effect = "walking";
                        Enum_effect = status_enum_effect.walking;
                        PlayEffectSound(status_effect);
                    }

                    //update old status;
                    previousEffect = Enum_effect;
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

            if (forloading == true)
                Debug.Log("soundData.soundFiles.player :" + soundData.soundFiles.player);


            AudioClip[] audioClips = Resources.LoadAll<AudioClip>(soundData.soundFiles.player);


            list_sort_audio.AddRange(audioClips);
            //    string clip1;


            // ?????? sort
            
            list_sort_audio.Sort((clip1, clip2) =>
            {
                //  string
                string clip1Name = clip1.name;
                string clip2Name = clip2.name;

                // if sort_data_sound_effect_player have data

                foreach (var sort_data_sound_effect_player in soundData.soundCategories.player)
                {
                    foreach(var audioClips1 in list_sort_audio)
                    {

                        int comparison1 = clip1Name.CompareTo(audioClips1.name.ToString());


                        int comparison2 = clip2Name.CompareTo(sort_data_sound_effect_player.id.ToString());

                        if (comparison1 != 0)
                        {
                            return comparison1; 
                        }
                        if (comparison2 != 0)
                        {
                            return comparison2;
                        }

                    }
                
                }

                // ?????? 0 ?????????????????
                return 0;
            });
            
            


            player_effect_sound = new Dictionary<string, AudioClip>();


      

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

            //if sound string have in list

            // ??????????????????????????????????? list ???????
            if (list_player_effect_sound.Contains(effectName))
            {
                //if effect sound is playing = false will try to get sound effect with string

                //but... what if effect sound is playing but have to change sound effect, what have to do?
                //you have to make new condition
                //may be have to remove audioSource.isPlaying because isplaying will true or false have to do this codition
                //may be have to use enum ...when change state will have to do this condition for play effect sound

                // ??????????????????????????? Dictionary ????????? AudioClip ?????
                if (player_effect_sound.TryGetValue(effectName, out AudioClip clip)  )
                {
                    // oldState = (int)Enum_effect;
                    audioSource.clip = null;

                    Debug.Log("Playeffect sound");

                    // Debug.Log(audioSource.)


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

  

    }
}
