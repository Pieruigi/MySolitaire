using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Zoca.Logic
{
    /// <summary>
    /// Each card is made by three fields: value, suit and color.
    /// Value is the main field: some cards may use only this one, for example memory cards.
    /// Suit is used by italian and french cards ( and others that need a suit together with a value ).
    /// Color is the deck color, and is used for example by french cards, when you use two decks.
    /// On french cards the wildcard has value = 0; the suit determines the wildcard color black or red ( not 
    /// the deck color, which is determined by the color field ).
    /// </summary>
    //[System.Serializable]
    public class Card
    {
        [SerializeField]
        public int Value
        {
            get { return value; }
        }

        [SerializeField]
        public int Suit
        {
            get { return suit; }
        }

        public int Color
        {
            get { return color; }
        }

        /// <summary>
        /// Value, suit and back color ( the last is used on french cards )
        /// </summary>
        int value, suit, color;

        public Card(int value, int suit = 0, int color = 0)
        {
            this.value = value;
            this.suit = suit;
            this.color = color;
        }

        /// <summary>
        /// For french cards
        /// </summary>
        /// <returns></returns>
        public bool IsWildcard()
        {
            return value == 0;
        }

        /// <summary>
        /// For french cards
        /// </summary>
        /// <returns></returns>
        public bool IsBlackWildcard()
        {
            return IsWildcard() && suit == 0;
        }

        /// <summary>
        /// For french cards
        /// </summary>
        /// <returns></returns>
        public bool IsRedWildcard()
        {
            return IsWildcard() && !IsBlackWildcard();
        }

        

        public override string ToString()
        {
            return string.Format("[Card Value:{0}, Suit:{1}]", value, suit);
        }
    }

}
