using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Logic;
using Zoca.Management;

namespace Zoca.UI
{
    public class ButtonPlay : MonoBehaviour
    {
        bool loading = false;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Play);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void Play()
        {
            if (loading)
                return;
            loading = true;

            if (GameManager.Instance.InGame)
            {
                bool show = false;

                if (Ruler.Instance.IsCompleted)
                {
                    show = true;
                }
                else
                {
                    show = Random.Range(0, 3) == 0 ? true : false;
                }

                // Check if the game is completed
                if (show)
                {
                    // Show interstitial
                    if (AdsManager.Instance.IsInterstitialLoaded())
                    {
                        if (!AdsManager.Instance.TryShowInterstitial(HandleOnInterstitialClosed))
                            StartCoroutine(LoadGameScene());
                    }
                    else
                    {
                        StartCoroutine(LoadGameScene());
                    }
                }
                else
                {
                    StartCoroutine(LoadGameScene());
                }
            }
            else
            {
                StartCoroutine(LoadGameScene());
            }
            
        }

        IEnumerator LoadGameScene()
        {
           
            yield return new WaitForSeconds(0.5f);
            
            GameManager.Instance.LoadSceneById(GameManager.GameSceneId);
        }

        void HandleOnInterstitialClosed()
        {
            StartCoroutine(LoadGameScene());
        }
    }

}
