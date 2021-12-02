using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zoca.Logic;

namespace Zoca.UI
{
    public class MySolitaireGameUI : GameUI
    {

        #region private fields
        [SerializeField]
        Transform mainDeckPivot;


        CardUI topDeckCard;

        

        #endregion

        #region protected methods
        protected override void Start()
        {
            base.Start();

            // Just cast the game logic instance for further use
            MySolitaireGameLogic gameLogic = (MySolitaireGameLogic)MySolitaireGameLogic.Instance;

            // Create a card and put it on the main deck pivot
            topDeckCard = Instantiate(CardPrefab, mainDeckPivot, false);

            // We need cards from piles, not from deck

            // Get the first card from the deck
            Card card = gameLogic.MainPile.GetCardAt(0);
            
            // Set the card in the ui
            topDeckCard.SetCard(card);
            topDeckCard.ShowBack();
        }


        public override void OnPointerDown(IPointerDownHandler handler, PointerEventData eventData)
        {
            throw new System.NotImplementedException();
            // I should call logic here
        }

        public override void OnPointerUp(IPointerUpHandler handler, PointerEventData eventData)
        {
            throw new System.NotImplementedException();
            // I should call logic here
        }

        #endregion
    }

}
