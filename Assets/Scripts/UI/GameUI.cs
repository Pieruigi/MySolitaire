using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Logic;

namespace Zoca.UI
{
    public class GameUI : MonoBehaviour
    {
        #region properties
        public GameObject CardPrefab
        {
            get { return cardPrefab; }
        }
        #endregion

        #region private fields
        [SerializeField]
        GameObject cardPrefab;

        [SerializeField]
        List<Interactor> interactors;

        Interactor selected = null;
        bool interactable = false;
        float moveTime = 1;
        GameObject movingObject;
        Interactor targetInteractor;
        #endregion

        private void Awake()
        {
            Ruler.Instance.DebugAll();
        }

        // Start is called before the first frame update
        void Start()
        {
            interactable = true;

            // Set custom selection effect for interactors
            for(int i=0; i<interactors.Count; i++)
            {
                if(i==0)
                    interactors[i].GetComponent<Interactor>().SetSelectionEffect(Interactor.SelectionEffect.FlipAndShake);
                else
                    interactors[i].GetComponent<Interactor>().SetSelectionEffect(Interactor.SelectionEffect.Shake);
            }
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public int GetIndex(Interactor interactor)
        {
            return interactors.FindIndex(i => i == interactor);
        }

        public void Interact(Interactor interactor)
        {
            Debug.LogFormat("[GameUI ClickingOn:{0}", interactor);


            // If the interactor is already selected and it's not the main pile you can unselect it
            if(selected == interactor)
            {
                if(GetIndex(interactor) != 0) 
                {
                    // Not the main pile interactor
                    selected.Unselect();
                    selected = null;
                }
                return;
            }

            // If there is another interactor selected you must check if you can move the selected card
            // on the new pile
            if(selected != null && selected != interactor)
            {
                CardPile sourcePile = Ruler.Instance.GetCardPileAt(GetIndex(selected));
                CardPile targetPile = Ruler.Instance.GetCardPileAt(GetIndex(interactor));
                if (Ruler.Instance.TryMoveCard(sourcePile, targetPile))
                {
                    Debug.LogFormat("GameUI - Card can be moved.");
                    // Unselect the card
                    selected.Unselect();

                    // Move the selected card to the new interactor
                    // Get target positions
                    Transform targetPivot = interactor.transform;
                    // Get the source card object
                    movingObject = selected.RemoveCardObject();
                    // Reset the selected interactor card object
                    selected.ResetCardObject();
                    // Set the target interactor in order to have it in the move callback
                    targetInteractor = interactor;
                    // Move 
                    movingObject.transform.DOMove(targetPivot.position, moveTime, true).OnComplete(HandleOnMoveComplete);

                    selected = null;
                }
                else
                {
                    Debug.LogFormat("GameUI - Card can not be moved.");
                }
                Ruler.Instance.DebugAll();
                return;
            }

            selected = interactor;
            interactor.Select();
        }

        void HandleOnMoveComplete()
        {
            // Destroy the moved object
            Destroy(movingObject);

            // Reset the target interactor
            targetInteractor.ResetCardObject();
            targetInteractor = null;
        }
    }

}
