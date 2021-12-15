using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zoca.Logic;

namespace Zoca.Management
{
    public class GameManager : MonoBehaviour
    {
        #region properties
        public static readonly int MainSceneId = 0;
        public static readonly int GameSceneId = 1;

        public static GameManager Instance { get; private set; }

        public bool InGame { get { return inGame; } }
        #endregion

        #region private fields

        bool inGame = false;
        #endregion

        #region private methods

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
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

        }

        // Update is called once per frame
        void Update()
        {

        }

        void HandleOnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
            if (scene.buildIndex == GameSceneId)
            {
                inGame = true;
            }
            else
            {
                inGame = false;

            }
        }

        #endregion

        #region public methods
        public void LoadSceneById(int id)
        {
            
            //Ruler.Destroy();

            SceneManager.LoadScene(id);

        }

        
        #endregion
    }

}
