using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zoca.Cards;
using Zoca.Logic;

namespace Zoca.Test
{
    public class TestDeck : MonoBehaviour
    {
        Deck deck;

        private void Awake()
        {
            //deck = ItalianCardUtility.CreateDeck();
            deck = FrenchCardUtility.CreateDeck(1, true);

            // Shuffle
            deck.Shuffle();

            // Log
            while (!deck.IsEmpty())
            {
                Card card = deck.GetCardAt(0);
                deck.RemoveCardAt(0);
                Debug.Log(card);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.LogFormat("[GameLogic type:{0}]", GameLogic.Instance.GetType());
        }

        // Update is called once per frame
        void Update()
        {

        }

       


    }

}
