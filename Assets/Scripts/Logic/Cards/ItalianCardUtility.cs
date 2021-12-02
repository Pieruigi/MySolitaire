using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Cards
{
    /// <summary>
    /// Can be used to manage italian cards type ( napoletane, piacentine etc ).
    /// Extends the CardUtility adding support for suit.
    /// They are represented by two elements in the card internal data:
    /// - value(int): the actual value of the card from 1 to 10 ( inherited by the BasicCardUtility )
    /// - suit(int): the suit of the card ( bastoni, denara, etc )
    /// </summary>
    public class ItalianCardUtility: CardUtility
    {
        //public static readonly int ValueIndex = 0;
        public static readonly int SuitIndex = 1;

        /// <summary>
        /// Create a deck using a specific range of cards and all the suits
        /// </summary>
        /// <returns></returns>
        public static new Deck CreateDeck(int minValue, int maxValue)
        {
            Deck deck = new Deck();

            for(int suit=0; suit<4; suit++)
            {
                for(int value=minValue; value<maxValue+1; value++)
                {
                    // Create internal representation
                    object[] dataArray = new object[] { value, suit };
                    Card card = new Card(dataArray);
                    // Add to deck
                    deck.AddCard(card);
                }
            }

            return deck;
        }

        /// <summary>
        /// Create a deck using all the available cards of a real italian deck.
        /// </summary>
        /// <returns></returns>
        public static Deck CreateDeck()
        {
            return CreateDeck(1, 10);
        }

               
        /// <summary>
        /// Returns the actual suit of the card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static int GetSuit(Card card)
        {
            return (int)card.GetDataAt(SuitIndex);
        }
    }

}
