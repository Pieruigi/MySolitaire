using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

            StartCoroutine(LoadGameScene());
        }

        IEnumerator LoadGameScene()
        {
            yield return new WaitForSeconds(0.5f);
            
            GameManager.Instance.LoadSceneById(GameManager.GameSceneId);
        }
    }

}
