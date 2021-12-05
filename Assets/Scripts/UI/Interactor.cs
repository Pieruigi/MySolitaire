using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Logic;

namespace Zoca.UI
{
    public class Interactor : MonoBehaviour
    {

        #region private fields
        [SerializeField]
        GameObject cardPrefab;


        CardPile pile;
        GameUI gameUI;
        GameObject cardObject; // The card to show
        #endregion

        #region private methods
        private void Awake()
        {
            gameUI = GetComponentInParent<GameUI>();
            // Set the card pile
            pile = Ruler.Instance.GetCardPileAt(gameUI.GetIndex(this));
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region public
        /// <summary>
        /// Show the front of the last card
        /// </summary>
        public void ShowFront()
        {
            if(cardObject == null)
                cardObject = Instantiate(cardPrefab, transform, false); // Create object
            
            // Set image

        }

        /// <summary>
        /// Show the back of the deck
        /// </summary>
        public void ShowBack()
        {

        }

        #endregion


    }

}
