using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zoca.Logic
{
    public enum GameResult { Defeat, Victory, Draw }

    //[System.Serializable]
    public class Ruler
    {
        

        public UnityAction<int> OnGameComplete;

        #region properties

        public static Ruler Instance
        {
            get 
            {
                if (instance == null)
                    instance = new Ruler();

                return instance;
            }
        }

        public int AttemptsLeft
        {
            get { return attemptsLeft; }
        }
        #endregion



        #region private fields
        
        /// <summary>
        /// Main, Discard, North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest
        /// </summary>
        //[SerializeField] 
        List<CardPile> piles;

        static Ruler instance = null;

        bool secondDeck = false;
        int attemptsLeft = 3;

        #endregion

        
        #region private methods
        protected Ruler()
        {
            // Create the deck
            List<Card> deck = CreateAndShuffleDeck();

            InitPiles(deck);

            // Test
            //piles[3].Add(new Card(1, 0));
            //piles[2].RemoveLast();
            //piles[2].Add(new Card(4, 0));
            //piles[2].Add(new Card(3, 0));
            //piles[2].Add(new Card(2, 0));
            //for (int i = 0; i < 28; i++)
            //{
            //    piles[0].RemoveLast();
            //}
        }

        void InitPiles(List<Card> deck)
        {
            piles = new List<CardPile>();

            //// Remove aces from deck
            //List<Card> aces = deck.FindAll(c => c.Value == 1);
            //foreach (Card c in aces)
            //{
            //    deck.Remove(c);
            //}

            // Create all the piles
            for (int i = 0; i < 10; i++)
            {
                CardPile pile = new CardPile();
                piles.Add(pile);


                switch (i)
                {
                    case 0: // The main pile, copy the deck
                        pile.AddRange(deck);
                        break;
                    case 2: // North
                    case 4: // East
                    case 6: // South
                    case 8: // West
                        // Get a card from the main pile
                        Card card = piles[0].RemoveLast();
                        pile.Add(card);
                        break;
                    //case 3:
                    //case 5:
                    //case 7:
                    //case 9:
                    //    Card ace = aces[0];
                    //    aces.RemoveAt(0);
                    //    pile.Add(ace);
                    //    break;
                }
            }
        }

        // Create the deck
        List<Card> CreateAndShuffleDeck()
        {
            List<Card> ret = new List<Card>();

            // Create
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

            // Shuffle
            List<Card> tmp = new List<Card>();
            while (ret.Count > 0)
            {
                Card card = ret[Random.Range(0, ret.Count)];
                tmp.Add(card);
                ret.Remove(card);
            }

            ret = tmp;

            return ret;
        }

        bool YouWin()
        {
            int nextId = 3;
            int step = 2;
            for(int i=0; i<4; i++)
            {
                CardPile pile = piles[nextId];

                if (pile.IsEmpty() || pile.Count < 10)
                    return false;

                nextId += step;
            }

            return true;
        }

        #endregion

        #region public methods
        
        /// <summary>
        /// Try to move the last card from the source pile to the target pile.
        /// </summary>
        /// <param name="sourcePile"></param>
        /// <param name="targetPile"></param>
        /// <returns></returns>
        public bool TryMoveCard(CardPile sourcePile, CardPile targetPile)
        {
            // Get the id of the card pile this card belongs to
            int sourceId = piles.FindIndex(p => p == sourcePile);

            // Get the new card pile id
            int targetId = piles.FindIndex(p=>p == targetPile);

            if (sourceId == targetId)
                return false;

            bool ret = false;
            
            //if(sourceId == 0 && targetId == 1)
            //{
            //    if (secondDeck)
            //        attemptsLeft--;
            //}

            switch (targetId)
            {
                case 0:
                    // You can not move to the main pile
                    break;
                case 1:
                    // You can always move one card to the discard pile
                    ret = true;
                    break;
                case 2: // North
                case 4: // East
                case 6: // South
                case 8: // West
                    // If the target pile is empty you can move
                    if (targetPile.IsEmpty())
                    {
                        ret = true;
                    }
                    else
                    {
                        // If the last card in the target pile has the same suit and value equal to card.value + 1
                        // you can move
                        if (targetPile.GetLast().Suit == sourcePile.GetLast().Suit &&
                            targetPile.GetLast().Value == sourcePile.GetLast().Value + 1)
                            ret = true;
                    }
                    
                    break;
                case 3: // North East
                case 5: // South East
                case 7: // South West
                case 9: // North West 
                    // If the target pile is empty and the card is an ace you can move
                    if (targetPile.IsEmpty() && sourcePile.GetLast().Value == 1)
                    {
                        ret = true;
                    }
                    else
                    {
                        // If the cards have the same suit and the target card value is equal to the source card
                        // value - 1 you can move
                        if (targetPile.GetLast().Suit == sourcePile.GetLast().Suit &&
                           targetPile.GetLast().Value == sourcePile.GetLast().Value - 1)
                            ret = true;
                    }
                   
                    break;
                
            }

            if (ret)
            {
                piles[targetId].Add(piles[sourceId].RemoveLast());
            }

            //if (secondDeck && attemptsLeft == 0)
            //{
            //    int result = YouWin() ? (int)GameResult.Victory : (int)GameResult.Defeat;

            //    OnGameComplete?.Invoke(result);
            //}
            
            if(YouWin())
                OnGameComplete?.Invoke((int)GameResult.Victory);

            return ret;
        }

        public bool IsSecondDeck()
        {
            return secondDeck;
        }

        public bool CheckFirstDeckCompleted()
        {
            
            if (piles[0].IsEmpty())
            {
                if (!secondDeck)
                {
                    // Move from discard to main pile
                    int count = piles[1].Count;
                    for (int i= 0; i< count ; i++)
                    {
                        piles[0].Add(piles[1].RemoveLast());
                    }
                    // Clear the discard pile
                    
                    secondDeck = true;
                }
                
            }
                
            
            return secondDeck;
        }

        

        public CardPile GetCardPileAt(int index)
        {
            return piles[index];
        }

        

        public void DebugAll()
        {
            for(int i=0; i<piles.Count; i++)
            {
                Debug.LogFormat("[CardPile {0}]", i);
                Debug.Log(piles[i]);
            }
        }

        #endregion

    }

}
