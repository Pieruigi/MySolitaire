using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Logic
{
    /// <summary>
    /// This class is used to make basic actions.
    /// Every time the game scene is loaded this class is instantiates and starts a new game ( so if you want to
    /// restart a game simply reload the game scene ).
    /// </summary>
    public abstract class GameLogic : MonoBehaviour
    {

        public static GameLogic Instance { get; private set; }

        protected virtual void Awake()
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
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        protected virtual void LateUpdate()
        {

        }
    }

}
