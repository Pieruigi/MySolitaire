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
        public abstract void Select(CardUI cardUI);
        public abstract void Unselect(CardUI cardUI);

        public abstract void Select(CardPileUI cardPileUI);
        public abstract void Unselect(CardPileUI cardPileUI);



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

        /// <summary>
        /// Called by the card ui when the player clicks on it.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="eventData"></param>
        #region public methods
        public bool IsInteractable(CardUI cardUI)
        {
            if (cardUI.Card.IsSelected() && !gameLogic.IsUnselectable(cardUI.Card))
                return false;
            if (!cardUI.Card.IsSelected() && !gameLogic.IsSelectable(cardUI.Card))
                return false;

            return true;
            
        }

        public bool IsInteractable(CardPileUI cardPileUI)
        {
            if (cardPileUI.CardPile.IsSelected() && !gameLogic.IsUnselectable(cardPileUI.CardPile))
                return false;
            if (!cardPileUI.CardPile.IsSelected() && !gameLogic.IsSelectable(cardPileUI.CardPile))
                return false;

            return true;

        }

        public virtual void OnPointerDown(IPointerDownHandler handler, PointerEventData eventData)
        {
            Debug.LogFormat("GameUI - On pointer down: {0}", handler);
            // Check which object the player clicked on
            
            // Card interaction
            if(((MonoBehaviour)handler).GetType().IsSubclassOf(typeof(CardUI)))
            {
                Debug.LogFormat("GameUI - Is CardUI type: {0}", true);
                // Clicked on a card, check if can be activated ( selected for example )
                // Get the card ui object
                CardUI cardUI = (CardUI)handler;
                
                if (!cardUI.Card.IsSelected())
                {
                    Debug.LogFormat("GameUI - Card is not selected: {0}", cardUI);
                    // The card is not selected, we check if it can be selected.
                    if (GameLogic.IsSelectable(cardUI.Card))
                    {
                        // The card can be selected.
                        // Implement the method in the child class.
                        GameLogic.Select(cardUI.Card);
                        // Get card target

                    }
                }
                else
                {
                    // The card is selected, we check if we can unselect it.
                    if(GameLogic.IsUnselectable(cardUI.Card))
                    {
                        // The card can be unselected.
                        // Implement the method in the child class.
                        GameLogic.Unselect(cardUI.Card);
                    }
                }
                
            }

            // Card pile interaction
            if (((MonoBehaviour)handler).GetType().IsSubclassOf(typeof(CardPileUI)))
            {
                Debug.Log("CardPileClicked");
            }


        }

        /// <summary>
        /// Called by the card ui when the player releases it.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="eventData"></param>
        public virtual void OnPointerUp(IPointerUpHandler handler, PointerEventData eventData)
        {
            
        }

        
        #endregion
    }

}
