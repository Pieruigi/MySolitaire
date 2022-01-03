using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Management
{
    public class AccountManager : MonoBehaviour
    {
        #region properties
        public static AccountManager Instance { get; private set; }

        public bool Logged { get; private set; }
        #endregion


        #region private fields

        float attemptTime = 5; // If not logged try every X seconds.
        #endregion

        #region private methods
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;

                if (!Logged)
                    LogIn();

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void LogIn()
        {
#if UNITY_ANDROID
            //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            //         enables saving game progress.
            //        .EnableSavedGames()
            //         requests the email address of the player be available.
            //         Will bring up a prompt for consent.
            //        .RequestEmail()
            //         requests a server auth code be generated so it can be passed to an
            //          associated back end server application and exchanged for an OAuth token.
            //        .RequestServerAuthCode(false)
            //         requests an ID token be generated.  This OAuth token can be used to
            //          identify the player to other services such as Firebase.
            //        .RequestIdToken()
            //        .Build();

            //GooglePlayGames.PlayGamesPlatform.InitializeInstance(config);
            // recommended for debugging:
            //GooglePlayGames.PlayGamesPlatform.DebugLogEnabled = true;
            // Activate the Google Play Games platform
            //GooglePlayGames.PlayGamesPlatform.Activate();

            //Social.Active.localUser.Authenticate((result) =>
            //{
            //    if (result)
            //        Logged = true;
            //    else
            //        Logged = false;


            //    Debug.Log("LogIn callback - Result:" + result);
            //});


            GooglePlayGames.PlayGamesPlatform.Instance.Authenticate(GooglePlayGames.BasicApi.SignInInteractivity.CanPromptAlways,
                (result) =>
                {
                    if (result == GooglePlayGames.BasicApi.SignInStatus.Success)
                        Logged = true;
                    else
                        Logged = false;


                    Debug.Log("LogIn callback - Result:" + (GooglePlayGames.BasicApi.SignInStatus)result);
                });
#endif

#if UNITY_IOS

#endif
        }

        
#endregion

#region public methods

#endregion
    }

}
