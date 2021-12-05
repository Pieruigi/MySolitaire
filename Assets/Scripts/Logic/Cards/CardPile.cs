using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zoca.Logic
{

    /// <summary>
    /// A deck is simply a collection of cards.
    /// It has some basic function, such as Shuffle(), Push() and Pop()
    /// </summary>
    public class CardPile
    {
        #region delegates
        public UnityAction OnSelected;
        public UnityAction OnUnselected;
        #endregion

        #region private fields
        List<Card> cards;
        bool selected = false;
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
        /// Get the last card of the pile, which is the last one pushed in.
        /// The card is not removed.
        /// </summary>
        /// <returns></returns>
        public Card GetLastCard()
        {
            if (cards.Count == 0)
                return null;

            return cards[cards.Count - 1];
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

        public void SetSelected(bool value)
        {
            selected = value;
            if (value)
                OnSelected?.Invoke();
            else
                OnUnselected?.Invoke();
        }



        public bool IsSelected()
        {
            return selected;
        }
        #endregion

    }

}
