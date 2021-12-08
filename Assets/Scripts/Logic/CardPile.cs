using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Logic
{
    public class CardPile
    {
        public int Count
        {
            get { return cards.Count; }
        }

        List<Card> cards;

        public CardPile()
        {
            cards = new List<Card>();
        }

        public void Clear()
        {
            cards.Clear();
        }

        public bool IsEmpty()
        {
            return cards.Count == 0;
        }

        public void Add(Card card)
        {
            cards.Add(card);
        }

        public Card GetLast()
        {
            return cards[cards.Count - 1];
        }

        public Card GetCardAt(int index)
        {
            return cards[index];
        }

        public Card RemoveLast()
        {
            Card card = GetLast();
            cards.Remove(card);
            return card;
        }

        public void AddRange(List<Card> cards)
        {
            this.cards.AddRange(cards);
        }

        public bool Contains(Card card)
        {
            return cards.Contains(card);
        }

        public override string ToString()
        {
            string ret = null;
            for(int i=0; i<cards.Count; i++)
            {
                if (i == 0)
                    ret = cards[i].ToString();
                else
                    ret += "\n" + cards[i].ToString();
            }
            return ret;
        }
    }

}
