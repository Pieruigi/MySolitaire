using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Management;

namespace Zoca.UI
{
    public class ButtonLeaderboard : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(HandleOnClick);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void HandleOnClick()
        {
            //if (!GooglePlayGames.PlayGamesPlatform.Instance.IsAuthenticated())
            //{
            //    return;
            //}

            LeaderboardManager.Instance.LoadScores();
            //LeaderboardManager.Instance.ReportScore(100);
        }
    }

}
