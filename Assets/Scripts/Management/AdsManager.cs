using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zoca.Management
{
    public class AdsManager : MonoBehaviour
    {
        #region events
        //public UnityAction OnInterstitialFailed;
        //public UnityAction OnInterstitialClosed;
        #endregion

        // appId = ca-app-pub-5242852541574124~6824983983

        #region properties
        public static AdsManager Instance { get; private set; }
        #endregion

        #region private fields
        
        bool initialized = false;

        InterstitialAd interstitial = null;

        DateTime lastCheckTime;
        bool interstitialLoading = false;
        float loadTime = 10f;
        UnityAction interstitialClosedCallback;
        #endregion

        #region private methods
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;

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
            MobileAds.Initialize(initStatus => 
            { 
                initialized = true; 

                // Preload the interstitial
                //LoadInterstitial(); 
            });
        }

        // Update is called once per frame
        void Update()
        {
            if((DateTime.UtcNow - lastCheckTime).TotalSeconds > loadTime)
            {
                // It's better to have the next ad already loaded when it comes to show the interstitial
                // to the player.
                lastCheckTime = DateTime.UtcNow;

                if (interstitialLoading)
                    return;

                if (interstitial != null && interstitial.IsLoaded())
                    return;

                PrefetchInterstitial();
            }
        }

        void PrefetchInterstitial()
        {
            
            interstitialLoading = true;



#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-5242852541574124/7810106057";
            //adUnitId = "ca-app-pub-3940256099942544/1033173712"; // Test id
#elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
                string adUnitId = "unexpected_platform";
#endif

            // Initialize an InterstitialAd.
            interstitial = new InterstitialAd(adUnitId);

            // Setting handles
            interstitial.OnAdLoaded += HandleOnInterstitialLoaded;
            interstitial.OnAdFailedToLoad += HandleOnInterstitialFailedToLoad;
            interstitial.OnAdOpening += HandleOnInterstitialOpening;
            interstitial.OnAdClosed += HandleOnInterstitialClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            interstitial.LoadAd(request);

        }

        #endregion


      


        #region callbacks
        /// <summary>
        /// Interstitial loading success callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void HandleOnInterstitialLoaded(object sender, EventArgs args)
        {
            Debug.LogFormat("Interstitial loaded");

            interstitialLoading = false;
        }

        /// <summary>
        /// Interstitial loading failed callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void HandleOnInterstitialFailedToLoad(object sender, EventArgs args)
        {
            Debug.LogFormat("Interstitial failed to load");

            interstitialLoading = false;
        }

        void HandleOnInterstitialOpening(object sender, EventArgs args)
        {
            Debug.LogFormat("Interstitial opening");

        }

        void HandleOnInterstitialClosed(object sender, EventArgs args)
        {
            Debug.LogFormat("Interstitial closed");
            //OnInterstitialClosed?.Invoke();
            interstitial = null;
            if (interstitialClosedCallback == null)
                return;
            interstitialClosedCallback?.Invoke();
            interstitialClosedCallback = null;
        }
        #endregion

        #region public methods
        public bool IsInterstitialLoaded()
        {
            if (interstitial != null && interstitial.IsLoaded())
                return true;

            return false;
        }

        /// <summary>
        /// Called by the app to show a preloaded interstitial
        /// </summary>
        public bool TryShowInterstitial(UnityAction closedCallback = null)
        {
            Debug.Log("Trying to show intertitial...");
            if (interstitial != null && interstitial.IsLoaded())
            {
                interstitialClosedCallback = closedCallback;
                interstitial.Show();
                return true;
            }
            else
            {
                return false;
             
            }
                
        }
        #endregion
    }

}
