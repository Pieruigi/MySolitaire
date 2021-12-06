using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zoca.Logic;

namespace Zoca.UI
{
    public class Interactor : MonoBehaviour, IPointerDownHandler
    {

        #region private fields
        
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
            if (!pile.IsEmpty())
            {
                // Create card 
                cardObject = Instantiate(gameUI.CardPrefab, transform, false); // Create object

                // Set the card
                cardObject.GetComponent<CardUI>().SetCard(pile.GetLast());

                // If is the main pile show the back, otherwise show the front
                if(gameUI.GetIndex(this) == 0)
                    cardObject.GetComponent<CardUI>().ShowBack();
                else
                    cardObject.GetComponent<CardUI>().ShowFront();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region public
        public void OnPointerDown(PointerEventData eventData)
        {
            gameUI.Interact(this);
        }

        public void Select()
        {
            Debug.Log("Selecting...");
            cardObject.GetComponent<CardUI>().Select();
            
        }

        public void Unselect()
        {
            Debug.Log("Unselecting...");
            cardObject.GetComponent<CardUI>().Unselect();
        }

        #endregion


    }

}
