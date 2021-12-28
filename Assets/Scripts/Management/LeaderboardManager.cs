using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Zoca.Management
{
    public class LeaderboardManager : MonoBehaviour
    {
        

        #region properties
        public static LeaderboardManager Instance { get; private set; }
        #endregion

        #region private fields
        string leaderboardId = "CgkI84qU0LYKEAIQAQ";

        #endregion

        #region private methods

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;


                

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

        
        void OnLeaderboardLoaded(IScore[] scores)
        {
            Debug.Log("OnLeaderboardLoaded - Scores.Length:" + scores.Length);
        }

        void OnScorePosted(bool result)
        {
            Debug.Log("OnScorePosted - Result: " + result);
        }
        #endregion

        #region public methods
        public void LoadScores()
        {
            
            Social.LoadScores(leaderboardId, OnLeaderboardLoaded);
        }

        public void ReportScore(int score)
        {
            Social.ReportScore(score, leaderboardId, OnScorePosted);
        }


        #endregion
    }

}