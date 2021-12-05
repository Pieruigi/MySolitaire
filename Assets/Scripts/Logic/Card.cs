using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Zoca.Logic
{
    public class Card
    {
        public int Value
        {
            get { return value; }
        }

        public int Suit
        {
            get { return suit; }
        }

        int value, suit;

        public Card(int value, int suit)
        {
            this.value = value;
            this.suit = suit;
        }

        public override string ToString()
        {
            return string.Format("[Card Value:{0}, Suit:{1}]", value, suit);
        }
    }

}
