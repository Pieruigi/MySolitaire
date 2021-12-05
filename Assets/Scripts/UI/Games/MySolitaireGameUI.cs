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
        CardPileUI mainCardPileUI;


        [SerializeField]
        CardPileUI northCardPileUI;
        [SerializeField]
        CardPileUI eastCardPileUI;
        [SerializeField]
        CardPileUI southCardPileUI;
        [SerializeField]
        CardPileUI westCardPileUI;


        CardUI topDeckCard;
        CardUI northCard;
        CardUI eastCard;
        CardUI southCard;
        CardUI westCard;


        #endregion

        #region protected methods
        protected override void Start()
        {
            base.Start();

            // Just cast the game logic instance for further use
            MySolitaireGameLogic gameLogic = (MySolitaireGameLogic)GameLogic;

            // Create a card and put it on the main deck pivot
            topDeckCard = Instantiate(CardPrefab, mainCardPileUI.transform, false);

            // We need cards from piles, not from deck

            // Get the first card from the deck
            Card card = gameLogic.MainPile.GetLastCard();
            
            // Set the card in the ui
            topDeckCard.SetCard(card);
            topDeckCard.ShowBack();

            // Init the card piles
            //northCardPileUI.SetCardPile(gameLogic.NorthPile);
            //eastCardPileUI.SetCardPile(gameLogic.EastPile);
            //southCardPileUI.SetCardPile(gameLogic.SouthPile);
            //westCardPileUI.SetCardPile(gameLogic.WestPile);

            //// Set cards ( I should let the card pile ui do this )
            //northCard = Instantiate(CardPrefab, northCardPileUI.transform, false);
            //northCard.SetCard(northCardPileUI.CardPile.GetLastCard());
            //northCard.ShowFront();
            //westCard = Instantiate(CardPrefab, westCardPileUI.transform, false);
            //westCard.SetCard(westCardPileUI.CardPile.GetLastCard());
            //westCard.ShowFront();
            //southCard = Instantiate(CardPrefab, southCardPileUI.transform, false);
            //southCard.SetCard(southCardPileUI.CardPile.GetLastCard());
            //southCard.ShowFront();
            //eastCard = Instantiate(CardPrefab, eastCardPileUI.transform, false);
            //eastCard.SetCard(eastCardPileUI.CardPile.GetLastCard());
            //eastCard.ShowFront();

        }




        #endregion


        #region public methods
        public override void Select(CardUI cardUI)
        {
            // Apply some effect
            Debug.LogFormat("Select cardUI:" + cardUI);
        }

        public override void Unselect(CardUI cardUI)
        {
            // Unapply the selected effect
            Debug.LogFormat("Unselect cardUI:" + cardUI);
        }

        public override void Select(CardPileUI cardPileUI)
        {
            // Apply some effect
            
        }

        public override void Unselect(CardPileUI cardPileUI)
        {
            // Unapply the selected effect
           
        }
        #endregion
    }

}
