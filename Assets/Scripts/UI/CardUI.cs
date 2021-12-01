using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Cards;

namespace Zoca.UI
{
    /// <summary>
    /// UI object to show cards.
    /// </summary>
    public class CardUI : MonoBehaviour
    {
        Card card; // The card connected to this ui

        Image image; // The image of the current ui

        private void Awake()
        {
            image = GetComponent<Image>();
        }


        #region public methods
        public void SetCard(Card card)
        {
            this.card = card;
        }

        public void ShowFront()
        {

        }

        public void ShowBack()
        {

        }
        #endregion
    }

}
