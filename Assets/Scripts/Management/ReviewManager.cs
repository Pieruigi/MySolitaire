using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zoca.Logic;

namespace Zoca.Management
{
    public class ReviewManager : MonoBehaviour
    {
        public UnityAction OnAskForReview;

        public static ReviewManager Instance { get; private set; }

        //ReviewManager reviewManager;

        [SerializeField]
        string playStoreUrl;

        [SerializeField]
        string appleStoreUrl;

        int reviewSteps = 4;
        string reviewStepsParam = "reviewSteps";
        string reviewedParam = "reviewed";


        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                //reviewManager = new ReviewManager();
                SceneManager.sceneLoaded += HandleOnSceneLoaded;
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
            //StartCoroutine(CheckForReview());
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        void HandleOnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
            if (Ruler.Instance)
            {
                Ruler.Instance.OnGameComplete += HandleOnGameCompleted;
            }
        }

        void HandleOnGameCompleted(int result)
        {
            bool rev = PlayerPrefs.GetInt(reviewedParam, 0) == 0 ? false : true;

            if (rev)
                return;

            // Load review steps from player prefs
            int steps = PlayerPrefs.GetInt(reviewStepsParam, 0);
            steps++;
            // Store
            PlayerPrefs.SetInt(reviewStepsParam, steps);

            if(steps % reviewSteps == 0)
            {
                // Pop up
                OnAskForReview?.Invoke();

            }

           
            
        }

        public void OpenReviewUrl()
        {
            PlayerPrefs.SetInt(reviewedParam, 1);

            string url = "";
#if UNITY_ANDROID
            url = playStoreUrl;
#endif
#if UNITY_IOS
            url = appleStoreUrl;
#endif

            Application.OpenURL(url);

        }

        //IEnumerator CheckForReview()
        //{
        //    Debug.Log("Checking for review....");
        //    var requestFlowOperation = reviewManager.RequestReviewFlow();
        //    yield return requestFlowOperation;
        //    if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        //    {
        //        // Log error. For example, using requestFlowOperation.Error.ToString().
        //        Debug.LogError("Launch operation flow error:" + requestFlowOperation.Error.ToString());
        //        yield break;
        //    }
        //    Debug.Log("Received play info");
        //    var playReviewInfo = requestFlowOperation.GetResult();

        //    var launchFlowOperation = reviewManager.LaunchReviewFlow(playReviewInfo);
        //    yield return launchFlowOperation;
        //    Debug.Log("Received launch operation flow");
        //    playReviewInfo = null; // Reset the object
        //    if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        //    {
        //        Debug.LogError("Launch operation flow error:" + launchFlowOperation.Error.ToString());
        //        // Log error. For example, using requestFlowOperation.Error.ToString().
        //        yield break;
        //    }

        //}

        
    }

}
