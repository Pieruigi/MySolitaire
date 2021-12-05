using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zoca.Logic;

namespace Zoca.UI
{
    /// <summary>
    /// The object you select if you want to play selected cards.
    /// </summary>
    public class CardPileUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        bool interactable = false;
        Image image;

        protected GameUI GameUI { get; private set; }

        public CardPile CardPile
        {
            get { return cardPile; }
        }
        
        public CardPile cardPile;

        #region private methods
        private void Awake()
        {
            GameUI = transform.root.GetComponent<GameUI>();
            image = GetComponent<Image>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Simply call the GameUI to apply some effect on selected.
        /// </summary>
        void HandleOnSelected()
        {
            GameUI.Select(this); // Just apply some effect 
            SetInteractable(GameUI.IsInteractable(this));
        }

        /// <summary>
        /// Simply call the GameUI to apply some effect on unselected.
        /// </summary>
        void HandleOnUnselected()
        {
            GameUI.Unselect(this); // Just apply some effect 
            SetInteractable(GameUI.IsInteractable(this));
        }
#endregion
        #region public methods
        public void SetCardPile(CardPile cardPile)
        {
            this.cardPile = cardPile;
            cardPile.OnSelected = HandleOnSelected;
            cardPile.OnUnselected = HandleOnUnselected;
            SetInteractable(GameUI.IsInteractable(this));
        }

        void SetInteractable(bool value)
        {
            image.enabled = false;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            if (!interactable)
                return;

            GameUI.OnPointerDown(this, eventData);
        }

        /// <summary>
        /// This is called every time you release from clicking.
        /// /// We don't try to access directly to the game logic, instead we call the GameUI
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!interactable)
                return;
            GameUI.OnPointerUp(this, eventData);
        }
        #endregion


    }

}
