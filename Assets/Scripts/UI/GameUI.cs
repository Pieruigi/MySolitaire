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
        GameObject interactorPrefab; 

        [SerializeField]
        List<Transform> interactorsPivots;

        [SerializeField]
        List<Interactor> interactors;

        Interactor selected = null;
        bool interactable = false;
        float moveTime = 1;
        GameObject toDestroy;
        
        #endregion

        private void Awake()
        {
            Ruler.Instance.DebugAll();
        }

        // Start is called before the first frame update
        void Start()
        {
            interactable = true;
            interactors = new List<Interactor>();
            // Create interactors
            for(int i=0; i<interactorsPivots.Count; i++)
            {
                interactors.Add(CreateInteractor(i));
            }

            
        }

        // Update is called once per frame
        void Update()
        {

        }

        Interactor CreateInteractor(int index)
        {
            Interactor interactor = Instantiate(interactorPrefab, interactorsPivots[index], false).GetComponent<Interactor>();

            // Try to set card
            if (!Ruler.Instance.GetCardPileAt(index).IsEmpty())
            {
                interactor.SetCard(Ruler.Instance.GetCardPileAt(index).GetLast());
                if (index == 0)
                {
                    interactor.ShowBack();
                }
                else
                {
                    interactor.ShowFront();
                }
            }
            else
            {
                // Empty, hide
                interactor.Hide();
            }

            // Effect and visibility
            if (index == 0)
            {
                interactor.SetSelectionEffect(Interactor.SelectionEffect.FlipAndShake);
            }
            else
            {
                interactor.SetSelectionEffect(Interactor.SelectionEffect.Shake);
            }

            return interactor;
        }

        public int GetIndex(Interactor interactor)
        {
            return interactors.FindIndex(i => i == interactor);
        }

        public void Interact(Interactor target)
        {
            Debug.LogFormat("[GameUI ClickingOn:{0}", target);


            // If the interactor is already selected and it's not the main pile you can unselect it
            if(selected == target)
            {
                if(GetIndex(target) != 0) 
                {
                    // Not the main pile interactor
                    selected.Unselect();
                    selected = null;
                }
                return;
            }

            // If there is another interactor selected you must check if you can move the selected card
            // on the new pile
            if(selected != null && selected != target)
            {

                CardPile sourcePile = Ruler.Instance.GetCardPileAt(GetIndex(selected));
                CardPile targetPile = Ruler.Instance.GetCardPileAt(GetIndex(target));
                if (Ruler.Instance.TryMoveCard(sourcePile, targetPile))
                {
                    Interactor sourceInteractor = selected;
                    Interactor targetInteractor = target;

                    // Get source and target interactor ids
                    int targetId = interactors.FindIndex(i => i == targetInteractor);
                    int sourceId = interactors.FindIndex(i => i == sourceInteractor);

                    // If the card was selected from the main deck we must change the effect in order to
                    // avoid the card to flip when unselected
                    if(sourceId == 0)
                    {
                        selected.SetSelectionEffect(Interactor.SelectionEffect.Shake);
                    }

                    Debug.LogFormat("GameUI - Card can be moved.");
                    // Unselect the card
                    selected.Unselect();

                    // Store the interactor we will destroy after the source interactor is in position
                    toDestroy = interactors[targetId].gameObject;

                    // Put the source interactor under the target pivot
                    interactors[targetId] = selected;
                    selected.transform.parent = interactorsPivots[targetId];

                    // Create a new interactor to replace the old one which is moving
                    interactors[sourceId] = CreateInteractor(sourceId);
                
                    // Move 
                    sourceInteractor.transform.DOMove(targetInteractor.transform.position, moveTime, true).OnComplete(HandleOnMoveComplete);

                    selected = null;
                }
                else
                {
                    Debug.LogFormat("GameUI - Card can not be moved.");
                }
                Ruler.Instance.DebugAll();
                return;
            }

            selected = target;
            target.Select();
        }

        void HandleOnMoveComplete()
        {
            // We need to destroy the old interactor in the target 
            Destroy(toDestroy);
            
        }
    }

}
