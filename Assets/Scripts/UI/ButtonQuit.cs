using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Management;

namespace Zoca.UI
{
    public class ButtonQuit : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Quit);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void Quit()
        {
            if (GameManager.Instance.InGame)
            {
                // Quit the current game
                GameManager.Instance.LoadSceneById(GameManager.MainSceneId);
            }
            else
            {
                // Quit the app
                Application.Quit();
            }
        }
    }

}
