using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zoca.Logic;

namespace Zoca.UI
{
    /// <summary>
    /// UI object to show cards.
    /// </summary>
    public class CardUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region propeties
        public Card Card
        {
            get { return card; }
        }

        public bool IsFront
        {
            get { return image.sprite == frontSprite; }
        }

        

        /// <summary>
        /// Only child can access this object
        /// </summary>
        protected GameUI GameUI { get; private set; }
       
        #endregion

        #region private fields
        Card card; // The card connected to this ui

        Image image; // The image of the current ui

        Sprite frontSprite, backSprite;
        bool interactable = false;
        #endregion

        #region protected methods
        protected virtual void Awake()
        {
            GameUI = GetComponentInParent<GameUI>();
            image = GetComponent<Image>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void LateUpdate()
        {

        }

        protected virtual Sprite GetBackSprite()
        {
            Sprite[] sprites = GameResourcesManager.Instance.GetSetOfCardsBackSprites();
            return sprites[0];
        }

        protected virtual Sprite GetFrontSprite()
        {
            Sprite[] sprites = GameResourcesManager.Instance.GetSetOfCardsFrontSprites();
            string valueStr = string.Format("{0:000}", CardUtility.GetValue(card));
            // Get the first sprite starting with the valueStr.
            // If the deck has multiple suits then the sprite with suit 000 is taken.
            return new List<Sprite>(sprites).Find(c => c.name.StartsWith(valueStr));
        }

        #endregion

        #region private methods
      
        /// <summary>
        /// Simply call the GameUI to apply some effect on selected.
        /// </summary>
        void HandleOnSelected()
        {
            GameUI.Select(this);
            SetInteractable(GameUI.IsInteractable(this));
        }

        /// <summary>
        /// Simply call the GameUI to apply some effect on unselected.
        /// </summary>
        void HandleOnUnselected()
        {
            GameUI.Unselect(this);
            SetInteractable(GameUI.IsInteractable(this));
        }

        /// <summary>
        /// Set the card not selectable
        /// </summary>
        /// <param name="value"></param>
        void SetInteractable(bool value)
        {
            image.raycastTarget = value;
        }
        #endregion

        #region public methods
        public void SetCard(Card card)
        {
            // Set the logic card
            this.card = card;
            // Set the handles
            card.OnSelected += HandleOnSelected;
            card.OnUnselected += HandleOnUnselected;
            SetInteractable(GameUI.IsInteractable(this));

            // Set front sprite
            frontSprite = GetFrontSprite();

            // Set back sprite
            backSprite = GetBackSprite(); 
            
        }

        public void ShowFront()
        {
            image.sprite = frontSprite;
            image.enabled = true;
        }

        public void ShowBack()
        {
            image.sprite = backSprite;
            image.enabled = true;
        }

        public void Hide()
        {
            image.enabled = false;
        }

        

        /// <summary>
        /// This method is called every time you click on a card ui.
        /// We don't try to access directly to the game logic, instead we call the GameUI
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            GameUI.OnPointerDown(this, eventData);
        }

        /// <summary>
        /// This is called every time you release from clicking.
        /// /// We don't try to access directly to the game logic, instead we call the GameUI
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            GameUI.OnPointerUp(this, eventData);
        }


        #endregion
    }

}
