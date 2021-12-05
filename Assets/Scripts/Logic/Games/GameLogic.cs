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
        //public void UnityAction<>

        #region properties

        #endregion


        #region private fields
        static GameLogic instance; // Singletone
        #endregion

        #region abstract methods
        

        /// <summary>
        /// It depends on the game implementation.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public abstract bool IsSelectable(Card card);

        /// <summary>
        /// It depends on the game implementation.
        /// For some games each card can always be deselected, for others this could not be possible ( for
        /// example in a game like memery when you select a card you flip it, so it can not be unselected ).
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public abstract bool IsUnselectable(Card card);

        /// <summary>
        /// Select the card and check action targets for activation.
        /// </summary>
        /// <param name="card"></param>
        public abstract void Select(Card card);

        /// <summary>
        /// Unselect the card and check action targets for deactivation.
        /// </summary>
        /// <param name="card"></param>
        public abstract void Unselect(Card card);

        public abstract bool IsSelectable(CardPile cardPile);

        public abstract bool IsUnselectable(CardPile cardPile);
                

        #endregion

        #region protected methods
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
        #endregion
    }

}
