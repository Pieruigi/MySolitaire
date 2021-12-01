using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zoca.Cards;

namespace Zoca.Logic
{
    public class MySolitaireGameLogic : GameLogic
    {

        Deck deck;

        protected override void Awake()
        {
            base.Awake();

            // Create an italian deck
            deck = ItalianCardUtility.CreateDeck();

            // Shuffle the deck
            deck.Shuffle();
        }
    }

}
