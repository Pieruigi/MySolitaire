using System;
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
        public enum SelectionEffect { None, Shake, Flip, FlipAndShake }

        
        #region private fields

        CardPile pile;
        GameUI gameUI;
        GameObject cardObject; // The card to show
        SelectionEffect selectionEffect = SelectionEffect.None;
        float interactionTime = 0.5f;
        DateTime lastInteractionTime;

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
            ResetCardObject();
        }

        // Update is called once per frame
        void Update()
        {
            
        }


        #endregion

        #region public
        public void ResetCardObject()
        {
            if (!pile.IsEmpty())
            {
                // Create card 
                cardObject = Instantiate(gameUI.CardPrefab, transform, false); // Create object

                // Set the card
                cardObject.GetComponent<CardUI>().SetCard(pile.GetLast());

                // If is the main pile show the back, otherwise show the front
                if (gameUI.GetIndex(this) == 0)
                    cardObject.GetComponent<CardUI>().ShowBack();
                else
                    cardObject.GetComponent<CardUI>().ShowFront();
            }
        }


        public GameObject RemoveCardObject()
        {
            GameObject ret = cardObject;

            cardObject = null;
           
            return ret;

        }
        
        

        public void OnPointerDown(PointerEventData eventData)
        {
            if ((DateTime.UtcNow - lastInteractionTime).TotalSeconds < interactionTime)
                return;

            lastInteractionTime = DateTime.UtcNow;
            gameUI.Interact(this);
        }

        public void Select()
        {

            Debug.Log("Selecting...");
            CardUI cardUI = cardObject.GetComponent<CardUI>();

            switch (selectionEffect)
            {
                case Interactor.SelectionEffect.Shake:
                    cardUI.StartShaking();
                    break;
                case Interactor.SelectionEffect.Flip:
                    StartCoroutine(cardUI.Flip());
                    break;
                case Interactor.SelectionEffect.FlipAndShake:
                    StartCoroutine(cardUI.FlipAndStartShaking());
                    break;
            }

        }

        public void Unselect()
        {
            Debug.Log("Unselecting...");
            CardUI cardUI = cardObject.GetComponent<CardUI>();
            switch (selectionEffect)
            {
                case Interactor.SelectionEffect.Shake:
                    cardUI.StopShaking();
                    break;
                case Interactor.SelectionEffect.Flip:
                    StartCoroutine(cardUI.Flip());
                    break;
                case Interactor.SelectionEffect.FlipAndShake:
                    StartCoroutine(cardUI.StopShakingAndFlip());
                    break;
            }
        }

        public void SetSelectionEffect(SelectionEffect effect)
        {
            selectionEffect = effect;
        }

        public SelectionEffect GetSelectionEffect()
        {
            return selectionEffect;
        }
        #endregion


    }

}
