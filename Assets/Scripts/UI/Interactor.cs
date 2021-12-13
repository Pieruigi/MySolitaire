using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zoca.Logic;

namespace Zoca.UI
{
    /// <summary>
    /// Interactor is used by the UI to interact with cards.
    /// </summary>
    public class Interactor : MonoBehaviour, IPointerDownHandler
    {
        public enum SelectionEffect { None, Shake, Flip, FlipAndShake , Pulse, FlipAndPulse  }


        #region private fields

        //[SerializeField]
        Card card;
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
        public void SetCard(Card card)
        {
            this.card = card;

            // Create card 
            cardObject = Instantiate(gameUI.CardPrefab, transform, false); // Create object

            // Set the card
            cardObject.GetComponent<CardUI>().SetCard(card);

        }
        
        public void ShowFront()
        {
            cardObject.GetComponent<CardUI>().ShowFront();
        }

        public void ShowBack()
        {
            cardObject.GetComponent<CardUI>().ShowBack();
        }

        public void Hide()
        {
            if(cardObject)
                cardObject.GetComponent<CardUI>().Hide();
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

            if (cardObject)
            {
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
                    case Interactor.SelectionEffect.Pulse:
                        cardUI.StartPulsing();
                        break;
                    case Interactor.SelectionEffect.FlipAndPulse:
                        StartCoroutine(cardUI.FlipAndStartPulsing());
                        break;
                }
            }
            

        }

        public void Unselect()
        {
            if (cardObject)
            {
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
                    case Interactor.SelectionEffect.Pulse:
                        cardUI.StopPulsing();
                        break;
                    case Interactor.SelectionEffect.FlipAndPulse:
                        StartCoroutine(cardUI.StopPulsingAndFlip());
                        break;
                }
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
