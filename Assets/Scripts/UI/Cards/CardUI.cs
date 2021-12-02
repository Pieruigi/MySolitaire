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
        #endregion

        #region private fields
        Card card; // The card connected to this ui

        Image image; // The image of the current ui

        Sprite frontSprite, backSprite;
        #endregion

        #region protected methods
        protected virtual void Awake()
        {
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
      
        #endregion

        #region public methods
        public void SetCard(Card card)
        {
            // Set the logic card
            this.card = card;

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

        public void Flip(float time)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            GameUI.Instance.OnPointerDown(this, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            GameUI.Instance.OnPointerDown(this, eventData);
        }


        #endregion
    }

}
