using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Logic;

namespace Zoca.UI
{
    public class GameCompleteUI : MonoBehaviour
    {
        [SerializeField]
        Text messageField;

        [SerializeField]
        GameObject panel;

        private void Awake()
        {
            Ruler.Instance.OnGameComplete += HandleOnGameComplete;
            panel.SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void HandleOnGameComplete(int gameResult)
        {
            switch ((GameResult)gameResult)
            {
                case GameResult.Defeat:
                    messageField.text = "You Lose";
                    break;
                case GameResult.Victory:
                    messageField.text = "You Win";
                    break;
                case GameResult.Draw:
                    messageField.text = "You Draw";
                    break;
            }

            panel.SetActive(true) ;
        }
    }

}
