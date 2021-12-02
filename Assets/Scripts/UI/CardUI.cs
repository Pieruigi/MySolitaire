using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Cards;

namespace Zoca.UI
{
    /// <summary>
    /// UI object to show cards.
    /// </summary>
    public class CardUI : MonoBehaviour
    {

        Card card; // The card connected to this ui

        Image image; // The image of the current ui

        Sprite frontSprite, backSprite;

        private void Awake()
        {
            image = GetComponent<Image>();
        }


        #region public methods
        public void SetCard(Card card)
        {
            // Set the logic card
            this.card = card;

            // Set front sprite
            Sprite[] tmp = GameResourcesManager.Instance.GetSetOfCardsFrontSprites();
            SetFrontSprite(tmp);

            // Set back sprite
            tmp = GameResourcesManager.Instance.GetSetOfCardsBackSprites();
            backSprite = tmp[0]; // No multiple deck color support
            
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

        protected virtual void SetFrontSprite(Sprite[] cards)
        {
            string valueStr = string.Format("{0:000}", card.GetDataAt(CardUtility.ValueIndex));
            // Get the first sprite starting with the valueStr.
            // If the deck has multiple suits then the sprite with suit 000 is taken.
            frontSprite = new List<Sprite>(cards).Find(c => c.name.StartsWith(valueStr));

        }

        
        #endregion
    }

}
