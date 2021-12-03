using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zoca.Logic;

namespace Zoca.UI
{
    public abstract class GameUI : MonoBehaviour
    {

        #region properties
        
        public CardUI CardPrefab
        {
            get { return cardPrefab; }
        }

        protected GameLogic GameLogic
        {
            get { return gameLogic; }
        } 
        #endregion

        #region private fields
        [SerializeField]
        CardUI cardPrefab; // The card used in the game


        GameLogic gameLogic;
        static GameUI instance; // Singletone
        #endregion

        #region protected methods
        public abstract void OnPointerDown(IPointerDownHandler handler, PointerEventData eventData);
        public abstract void OnPointerUp(IPointerUpHandler handler, PointerEventData eventData);

        /// <summary>
        /// This method can be overriden by the child.
        /// </summary>
        protected virtual void Awake()
        {
            if (!instance)
            {
                instance = this;

                GameLogic.OnCreate += HandleOnGameLogicCreate;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// This method can be overriden by the child.
        /// </summary>
        protected virtual void Start()
        {
           
        }

        /// <summary>
        /// This method can be overriden by the child.
        /// </summary>
        protected virtual void Update()
        {

        }

        /// <summary>
        /// This method can be overriden by the child.
        /// </summary>
        protected virtual void LateUpdate()
        {

        }
        #endregion

        #region private
        void HandleOnGameLogicCreate(GameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
        }
        #endregion
    }

}
