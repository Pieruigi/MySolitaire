using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zoca.Logic;

namespace Zoca.Test
{

  
  
    public class TestDeck : MonoBehaviour
    {
        CardPile deck;

        private void Awake()
        {
            deck = ItalianCardUtility.CreateDeck();
            //deck = FrenchCardUtility.CreateDeck(1, true);

            // Shuffle
            deck.Shuffle();

            // Log
            while (!deck.IsEmpty())
            {
                Card card = deck.PopCard();
                
                Debug.Log(card);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
          
        }

        // Update is called once per frame
        void Update()
        {

        }

       


    }

}
