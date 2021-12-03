using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zoca.Logic
{
    /// <summary>
    /// This class is used to make basic actions.
    /// Every time the game scene is loaded this class is instantiates and starts a new game ( so if you want to
    /// restart a game simply reload the game scene ).
    /// </summary>
    public abstract class GameLogic : MonoBehaviour
    {
        public static UnityAction<GameLogic> OnCreate;

        #region properties

        #endregion


        #region private fields
        static GameLogic instance; // Singletone
        #endregion

        protected virtual void Awake()
        {
            if (!instance)
            {
                instance = this;
                OnCreate?.Invoke(this);
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
