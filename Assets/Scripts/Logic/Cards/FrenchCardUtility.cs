using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Cards
{
    /// <summary>
    /// Can be used to manage french cards type.
    /// Extends the ItalianCardUtility adding support for deck color.
    /// They are represented by three elements:
    /// - value(int): the actual value of the card from 1 to 13; -1 black wildcard; -2 red wildcard
    /// - suit(int): the suit of the card ( spades, diamonds, etc )
    /// - color(int): the deck color; 0 for blue and 1 for red;
    /// 
    /// NB: wildcard only has two elements, the value ( -1 or -2 ) and the color
    /// </summary>
    public class FrenchCardUtility: ItalianCardUtility
    {
        public static readonly int DeckColorIndex = 2;
        public static readonly int WildcardDeckColorIndex = 1;

        public static readonly int BlackWildcard = -1;
        public static readonly int RedWildcard = -2;

        public static readonly int BlueDeckColor = 0;
        public static readonly int RedDeckColor = 1;

        /// <summary>
        /// Create a deck using all the available cards of a real french deck.
        /// You must specify if you want wildcards too.
        /// </summary>
        /// <returns></returns>
        public static Deck CreateDeck(int color = 0, bool wildcards = false)
        {
            return CreateDeck(1, 13, color, wildcards);
        }


        /// <summary>
        /// Create a deck using a specific range of cards and all the suits; you can specify the color of
        /// the deck.
        /// You can also use the CreateDeck() method of the ItalianCardUtility which doesn't take into 
        /// account the deck color at all ( this means that the array is also made by two elements ); in this
        /// case wildcards can't be added to the deck and we always assume the deck color is blue.
        /// </summary>
        /// <returns></returns>
        public static Deck CreateDeck(int minValue, int maxValue, int deckColor = 0, bool wildcards = false)
        {
            Deck deck = new Deck();

            // Create the basic deck
            for(int suit=0; suit<4; suit++)
            {
                for(int value=minValue; value<maxValue+1; value++)
                {
                    // Create internal representation
                    object[] dataArray = new object[] { value, suit, deckColor };
                    Card card = new Card(dataArray);
                    // Add to deck
                    deck.AddCard(card);
                }
            }

            // Check for wildcards
            if (wildcards)
            {
                // Add black and red wildcards to the deck
                deck.AddCard(new Card(new object[] { BlackWildcard, deckColor }));
                deck.AddCard(new Card(new object[] { RedWildcard, deckColor }));
            }

            return deck;
        }

       
        /// <summary>
        /// Returns true if the card is a wildcard, otherwise returns false.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool IsWildcard(Card card)
        {
            return GetValue(card) < 0;
        }

        /// <summary>
        /// Returns true if is the black wildcard
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool IsBlackWildcard(Card card)
        {
            return GetValue(card) == BlackWildcard;
        }

        /// <summary>
        /// Returns true if is the black wildcard
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool IsRedWildcard(Card card)
        {
            return GetValue(card) == RedWildcard;
        }

        /// <summary>
        /// Returns the deck color of the card.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static int GetDeckColor(Card card)
        {
            if (!IsWildcard(card))
                return (int)card.GetDataAt(DeckColorIndex);
            else
            {
                
                if (card.GetDataArrayLength() == 1)
                {
                    // Deck has been created using the ItalianCardUtility.CreateDeck() which only sets
                    // value and suit, so we assume the deck color is blue.
                    return 0;
                }
                else
                {
                    // Created using the CreateDeck() of this class.
                    return (int)card.GetDataAt(WildcardDeckColorIndex);
                }
                    
            }
                
        }

        /// <summary>
        /// Returns true if the deck color of the card is blue
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool IsBlueDeckColor(Card card)
        {
            return GetDeckColor(card) == BlueDeckColor;
        }

        /// <summary>
        /// Returns true if the deck color of the card is red
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool IsRedDeckColor(Card card)
        {
            return GetDeckColor(card) == RedDeckColor;
        }
    }

}
