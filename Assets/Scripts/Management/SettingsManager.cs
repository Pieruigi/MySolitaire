using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Zoca.Management
{
    public class SettingsManager: MonoBehaviour
    {
        #region properties
        // Instance field
        public static SettingsManager Instance { get; private set; }
        
        // Settings
        public float GameSpeed
        {
            get { return gameSpeed; }
        }

        public int Difficulty
        {
            get { return difficulty; }
        }

        public int DeckId
        {
            get { return deckId; }
        }
        #endregion

        #region private fields
        [SerializeField]
        AudioMixer audioMixer;

        // Common
        bool saveEnabled = false;

        // Game speed
        float gameSpeed;
        string gameSpeedParamName = "GameSpeed";
        float gameSpeedNormal = 1;
        float gameSpeedFast = 2;

        // Audio
        float masterVolume = 0;
        string masterVolumeParamName = "MasterVolume";

        /// <summary>
        /// Not mandatory, just if you want to add some difficulty levels.
        /// 0 to N, from easy to hard
        /// </summary>
        int difficulty = 1;
        string difficultyParamName = "Difficulty";

        /// <summary>
        /// Deck type:
        /// 0 - Naples
        /// 1 - French
        /// </summary>
        int deckId = 0;
        string deckIdParamName = "DeckId";
        #endregion

        #region private methods
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;

                // Init game speed
                InitGameSpeed();

                // Init audio
                InitMasterVolume();

                // Difficulty
                InitDifficulty();

                // Deck
                InitDeck();

                saveEnabled = true;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        private void Start()
        {
            // You can't set mixer parameters in the awake, so you need to do it here
            float mv = masterVolume;
            if(audioMixer.GetFloat(masterVolumeParamName, out mv) && masterVolume != mv)
            {
                audioMixer.SetFloat(masterVolumeParamName, masterVolume);
            }
            
        }

        void InitGameSpeed()
        {
            if (PlayerPrefs.HasKey(gameSpeedParamName))
                InternalSetGameSpeed(PlayerPrefs.GetFloat(gameSpeedParamName));
            else
                InternalSetGameSpeed(gameSpeedNormal);
        }

        void InitMasterVolume()
        {
            if (PlayerPrefs.HasKey(masterVolumeParamName))
                InternalSetMasterVolume(PlayerPrefs.GetFloat(masterVolumeParamName));
            else
                InternalSetMasterVolume(0);
        }

        void InitDifficulty()
        {
            if (PlayerPrefs.HasKey(difficultyParamName))
                InternalSetDifficulty(PlayerPrefs.GetInt(difficultyParamName));
            else
                InternalSetDifficulty(difficulty);
        }

        void InitDeck()
        {
            if (PlayerPrefs.HasKey(deckIdParamName))
                InternalSetDeckId(PlayerPrefs.GetInt(deckIdParamName));
            else
                InternalSetDeckId(deckId);
        }

        void InternalSetDeckId(int deckId)
        {
            this.deckId = deckId;

            if(saveEnabled)
            {
                PlayerPrefs.SetInt(deckIdParamName, deckId);
                PlayerPrefs.Save();
            }
        }

        void InternalSetGameSpeed(float gameSpeed)
        {
            // Set value and timescale
            this.gameSpeed = gameSpeed;
           
            if (saveEnabled)
            {
                PlayerPrefs.SetFloat(gameSpeedParamName, gameSpeed);
                PlayerPrefs.Save();
            }
                
        }

        void InternalSetMasterVolume(float masterVolume)
        {
            this.masterVolume = masterVolume;
            // Set mixer
            audioMixer.SetFloat(masterVolumeParamName, masterVolume);

            if (saveEnabled)
            {
                PlayerPrefs.SetFloat(masterVolumeParamName, masterVolume);
                PlayerPrefs.Save();
            }
        }

        void InternalSetDifficulty(int difficulty)
        {
            this.difficulty = difficulty;

            if (saveEnabled)
            {
                PlayerPrefs.SetInt(difficultyParamName, difficulty);
                PlayerPrefs.Save();
            }
        }

        #endregion

        #region public methods
        public void SetGameSpeedNormal()
        {
            InternalSetGameSpeed(gameSpeedNormal);
        }

        public void SetGameSpeedFast()
        {
            InternalSetGameSpeed(gameSpeedFast);
        }

        public bool IsGameSpeedFast()
        {
            return gameSpeed == gameSpeedFast;
        }

        public void SetMasterVolumeOn()
        {
            InternalSetMasterVolume(0);
        }

        public void SetMasterVolumeOff()
        {
            InternalSetMasterVolume(-80);
        }

        public bool IsMasterVolumeOn()
        {
            return masterVolume == 0;
        }

        public void SetDifficulty(int difficulty)
        {
            InternalSetDifficulty(difficulty);
        }

        public void SetDeckId(int deckId)
        {
            InternalSetDeckId(deckId);
        }

        #endregion
    }

}
