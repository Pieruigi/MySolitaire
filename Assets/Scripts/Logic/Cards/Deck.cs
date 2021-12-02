using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Cards
{

    /// <summary>
    /// A deck is simply a collection of cards.
    /// It has some basic function, such as ShuffleCards() and GetTheFirstCard()
    /// </summary>
    public class Deck
    {
        #region private fields
        List<Card> cards;
        #endregion

        #region public methods
        public Deck()
        {
            cards = new List<Card>();
        }

        /// <summary>
        /// Add a new card to the deck.
        /// This can be used every time a new game starts to create a specific deck.
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        /// <summary>
        /// Returns true if the deck is empty, otherwise false
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return cards.Count == 0;
        }

        /// <summary>
        /// Gets the card at a specific index
        /// </summary>
        /// <returns></returns>
        public Card GetCardAt(int index)
        {
            // The first card of the dack
            Card ret = cards[index];

            return ret;
        }

        /// <summary>
        /// Removes the card at a specific index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveCardAt(int index)
        {
            cards.RemoveAt(index);
        }

        /// <summary>
        /// Shuffles the deck
        /// </summary>
        public void Shuffle()
        {
            // Temporary list
            List<Card> tmp = new List<Card>();

            // Add a random card from the deck to the tmp list
            while(cards.Count > 0)
            {
                Card card = cards[Random.Range(0, cards.Count)];
                tmp.Add(card);
                cards.Remove(card);
            }

            // Save tmp in deck
            cards = tmp;
        }

        #endregion

    }

}
