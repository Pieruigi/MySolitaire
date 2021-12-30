using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Management;

namespace Zoca.UI
{
    public class ButtonQuit : MonoBehaviour
    {
        bool leaving = false;

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
            if (leaving)
                return;
            leaving = true;

            if (GameManager.Instance.InGame)
            {
                // Quit the current game
                StartCoroutine(LoadMainScene());
            }
            else
            {
                // Quit the app
                //Application.Quit();
                StartCoroutine(QuitApplication());
            }
        }

        IEnumerator LoadMainScene()
        {
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.LoadSceneById(GameManager.MainSceneId);
        }

        IEnumerator QuitApplication()
        {
            yield return new WaitForSeconds(0.5f);
            Application.Quit();
        }
    }

}
