using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Logic
{

    /// <summary>
    /// This utility is used to manage the internal representation of cards that only have one value ( so 
    /// the internal representation is only made by an element ).
    /// This can be used for example to represent cards for the memory game.
    /// Internal representation:
    ///     value(int)
    /// </summary>
    public class CardUtility
    {

        //BasicCardUtility instance;
        
        public static readonly int ValueIndex = 0;

        

        /// <summary>
        /// Create a deck using a specific range
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static CardPile CreateDeck(int minValue, int maxValue)
        {
            CardPile deck = new CardPile();

            // Fill the deck
            int start = minValue;
            int end = maxValue + 1;

            for(int i=start; i<end; i++)
            {
                // Create a new card
                Card card = new Card(new object[] { i });

                // Add to deck
                deck.PushCard(card);
            }

            return deck;
        }

        public static int GetValue(Card card)
        {
            return (int)card.GetDataAt(ValueIndex);
        }

    }

}
