using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Logic
{
    public class MySolitaireGameLogic : GameLogic
    {
        #region properties

        public CardPile MainPile { get; private set; }
        public CardPile DiscardPile { get; private set; } = new CardPile();

        public IList<CardPile> CrossPiles { get; private set; } = new List<CardPile>();
        //public CardPile NorthPile { get; private set; } = new CardPile();
        //public CardPile EastPile { get; private set; } = new CardPile();
        //public CardPile SouthPile { get; private set; } = new CardPile();
        //public CardPile WestPile { get; private set; } = new CardPile();
        // Aces
        public CardPile NorthEastPile { get; private set; } = new CardPile();
        public CardPile SouthEastPile { get; private set; } = new CardPile();
        public CardPile SouthWestPile { get; private set; } = new CardPile();
        public CardPile NorthWestPile { get; private set; } = new CardPile();
        #endregion

        #region private fields






        #endregion

        #region protected methods
        protected override void Awake()
        {
            base.Awake();

            // Create deck and fill the main pile
            MainPile = ItalianCardUtility.CreateDeck();

            // Shuffle deck
            MainPile.Shuffle();

            // Init cross piles
            for(int i=0; i<4; i++)
            {
                CardPile pile = new CardPile();
                pile.PushCard(MainPile.PopCard());
                CrossPiles.Add(pile);
            }

        }


       
        #endregion

        #region public methods
       

        public override bool IsSelectable(Card card)
        {


            Debug.Log("Check IsSelectable:" + card);
            Debug.Log("MainPile.lastCard:" + MainPile.GetLastCard());
            if (card == MainPile.GetLastCard())
            {
                return true;
            }
           
            return false;
        }

        public override bool IsSelectable(CardPile cardPile)
        {
            return !cardPile.IsSelected();
        }

        public override bool IsUnselectable(Card card)
        {
            return card.IsSelected();
        }

        

        public override bool IsUnselectable(CardPile cardPile)
        {
            return cardPile.IsSelected();
        }

        public override void Select(Card card)
        {
            // Do some stuff here
            Debug.LogFormat("MySolitaireGameLogic - Select card: {0}", card);
            card.SetSelected(true);

        }

        public override void Unselect(Card card)
        {
            // Do some stuff here
            Debug.LogFormat("MySolitaireGameLogic - Unselect card: {0}", card);
            card.SetSelected(false);
        }
        #endregion

    }

}
