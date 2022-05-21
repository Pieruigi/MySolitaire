using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zoca.Logic;

namespace Zoca.Management
{
    public class AdsLaunchManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Ruler.Instance.OnGameComplete += HandleOnGameCompleted;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void HandleOnGameCompleted(int result)
        {
            if (AdsManager.Instance.IsInterstitialLoaded())
            {
                AdsManager.Instance.TryShowInterstitial();
                    
            }
        }
    }

}
