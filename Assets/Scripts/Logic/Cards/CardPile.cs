using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Logic
{

    /// <summary>
    /// A deck is simply a collection of cards.
    /// It has some basic function, such as Shuffle(), Push() and Pop()
    /// </summary>
    public class CardPile
    {
        #region private fields
        List<Card> cards;
        #endregion

       

        #region public methods
        public CardPile()
        {
           
            cards = new List<Card>();
        }

        /// <summary>
        /// Add a new card to the top.
        /// </summary>
        /// <param name="card"></param>
        public void PushCard(Card card)
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
        /// Returns a card at a specific index without removing it.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Card GetCardAt(int index)
        {
            return cards[index];
        }

        /// <summary>
        /// Removes the last cards ( which is the last pushed in )
        /// </summary>
        /// <param name="index"></param>
        public Card PopCard()
        {
            Card card = cards[cards.Count-1];
            cards.RemoveAt(cards.Count-1);
            return card;
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
