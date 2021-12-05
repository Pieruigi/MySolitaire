using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Logic
{
    public class Ruler
    {
        public static Ruler Instance
        {
            get 
            {
                if (instance == null)
                    instance = new Ruler();

                return instance;
            }
        }

        static Ruler instance = null;

        private Ruler()
        {

        }

        // Create the deck
        List<Card> CreateDeck()
        {
            List<Card> ret = new List<Card>();

            for(int i=0; i<10; i++)
            {
                // Values
                for(int j=0; j<4; j++)
                {
                    // Suits
                    Card card = new Card(i + 1, j);
                    ret.Add(card);
                }
            }

            return ret;
        }

    }

}
