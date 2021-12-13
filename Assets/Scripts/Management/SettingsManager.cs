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
            set { SetGameSpeed(value); }
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
        
        #endregion

        #region private methods
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;

                // Get game speed from player prefs
                if (PlayerPrefs.HasKey(gameSpeedParamName))
                    SetGameSpeed(PlayerPrefs.GetFloat(gameSpeedParamName));
                else
                    SetGameSpeed(gameSpeedNormal);


                saveEnabled = true;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        void SetGameSpeed(float gameSpeed)
        {
            // Set value and timescale
            this.gameSpeed = gameSpeed;
           
            if (saveEnabled)
            {
                PlayerPrefs.SetFloat(gameSpeedParamName, gameSpeed);
                PlayerPrefs.Save();
            }
                
        }

        void SetMasterVolume(float masterVolume)
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

        #endregion

        #region public methods
        public void SetNormalGameSpeed()
        {
            SetGameSpeed(gameSpeedNormal);
        }

        public void SetFastGameSpeed()
        {
            SetGameSpeed(gameSpeedFast);
        }

        public bool IsGameSpeedFast()
        {
            return gameSpeed == gameSpeedFast;
        }

        public void SetMasterVolumeOn()
        {
            masterVolume = 0;
        }

        public void SetMasterVolumeOff()
        {
            masterVolume = -80;
        }

        public bool IsMasterVolumeOn()
        {
            return masterVolume == 0;
        }
        #endregion
    }

}
