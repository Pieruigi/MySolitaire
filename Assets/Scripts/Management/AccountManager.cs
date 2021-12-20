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
            GooglePlayGames.PlayGamesPlatform.Instance.Authenticate(GooglePlayGames.BasicApi.SignInInteractivity.CanPromptAlways, 
                (result) => 
                {
                    if (result == GooglePlayGames.BasicApi.SignInStatus.Success)
                        Logged = true;
                    else
                        Logged = false;

                    
                    Debug.Log("LogIn callback - Result:" + (GooglePlayGames.BasicApi.SignInStatus)result);
                } );
#endif

#if UNITY_IOS

#endif
        }
#endregion

#region public methods

#endregion
    }

}
