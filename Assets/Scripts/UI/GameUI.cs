using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Logic;
using Zoca.Management;

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

        //[SerializeField]
        List<Interactor> interactors;

        //[SerializeField]
        //Ruler ruler;

        Interactor selected = null;
        bool interactable = false;
        float moveTime = .2f;
        GameObject toDestroy;
        float gameSpeed = 1;
        #endregion

        #region private methods
        private void Awake()
        {
            //ruler = Ruler.Instance;
            //Ruler.Instance.DebugAll();
            //Ruler.Instance.OnGameComplete += HandleOnGameComplete;

        }

        // Start is called before the first frame update
        void Start()
        {
            // Setting time scale 
            Time.timeScale = SettingsManager.Instance.GameSpeed;

            Ruler.Instance.OnGameComplete += HandleOnGameComplete;

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

        void HandleOnGameComplete(int gameResult)
        {
            interactable = false;
        }

        bool TryMove(Interactor sourceInteractor, Interactor targetInteractor)
        {
            // Get source and target interactor ids
            int targetId = GetIndex(targetInteractor);
            int sourceId = GetIndex(sourceInteractor);

            CardPile sourcePile = Ruler.Instance.GetCardPileAt(sourceId);
            CardPile targetPile = Ruler.Instance.GetCardPileAt(targetId);

            if (Ruler.Instance.TryMoveCard(sourcePile, targetPile))
            {
                interactable = false;

                // If the card was selected from the main deck we must change the effect in order to
                // avoid the card to flip when unselected
                if (sourceId == 0)
                {
                    selected.SetSelectionEffect(Interactor.SelectionEffect.Pulse);
                }

                Debug.LogFormat("GameUI - Card can be moved.");

                // Store the interactor we will destroy after the source interactor is in position
                toDestroy = interactors[targetId].gameObject;

                // Put the source interactor under the target pivot
                interactors[targetId] = sourceInteractor;
                sourceInteractor.transform.parent = interactorsPivots[targetId];

                // Create a new interactor to replace the old one which is moving
                interactors[sourceId] = CreateInteractor(sourceId);

                // Move 
                (sourceInteractor.transform.parent as RectTransform).SetAsLastSibling();
                sourceInteractor.transform.DOMove(targetInteractor.transform.position, moveTime, true).OnComplete(() => HandleOnMoveComplete(sourceId, targetId));

                return true;
            }
            else
            {
                Debug.LogFormat("GameUI - Card can not be moved.");
                return false;
            }
        }

        void HandleOnMoveComplete(int sourceId, int targetId)
        {
            // We need to destroy the old interactor in the target 
            Destroy(toDestroy);

            Debug.LogFormat("GameUI - Move completed, sourceId:{0}, targetId:{1}", sourceId, targetId);

            //
            // If the pile is neither the discard pile nor the main pile we must check if there are other cards 
            // left in order to move them too.
            //
            // Source was main pile or discard pile, so return
            if (sourceId == 0 || sourceId == 1)
            {
                // If you picked the card from the main pile you must check if it was the last one; if so you
                // need to move all the cards back from the discard pile to the main pile.
                if (!Ruler.Instance.IsSecondDeck())
                {
                    if (Ruler.Instance.CheckFirstDeckCompleted())
                    {
                        StartCoroutine(MoveBackFromDiscardPile());
                    }
                    else
                    {
                        interactable = true;
                    }
                        
                }
                else
                {
                    if (Ruler.Instance.AttemptsLeft > 0)
                        interactable = true;
                    
                    
                }
                return;
            }

            // Avoid to move a complete pile to the discard pile 
            if(targetId == 1)
            {
                interactable = true;
                return;
            }

            // Pile is empty, return
            if (Ruler.Instance.GetCardPileAt(sourceId).IsEmpty())
            {
                interactable = true;
                return;
            }

            // Source pile is not empty, we must move card until it is empty
            if (!TryMove(interactors[sourceId], interactors[targetId]))
            {
                interactable = true;
            }

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
                interactor.SetSelectionEffect(Interactor.SelectionEffect.FlipAndPulse);
            }
            else
            {
                interactor.SetSelectionEffect(Interactor.SelectionEffect.Pulse);
            }

            return interactor;
        }

        IEnumerator MoveBackFromDiscardPile()
        {
            yield return new WaitForSeconds(1 / gameSpeed);

           

            // How many cards we need to animate ?
            int count = 0;
            int totalCards = Ruler.Instance.GetCardPileAt(0).Count;

            // Destroy the interactor in the main pile
            Destroy(interactors[0].gameObject);

            // Our source is the interactor from the discard pile
            Interactor sourceInteractor = interactors[1];

            while(sourceInteractor)
            {
                // Create an interactor under the source interactor if there are at least 2 cards to move
                Interactor nextInteractor = null;
                if (count <= totalCards - 2)
                {
                    nextInteractor = Instantiate(interactorPrefab, interactorsPivots[1], false).GetComponent<Interactor>();
                    // Move back in the hierarchy
                    (nextInteractor.transform as RectTransform).SetAsFirstSibling();
                    nextInteractor.SetCard(Ruler.Instance.GetCardPileAt(0).GetCardAt(count+1));
                    nextInteractor.ShowFront();
                    Debug.Log("New card created:" + nextInteractor);
                }


                // Move the source interactor
                interactors[0] = sourceInteractor;
                sourceInteractor.transform.parent = interactorsPivots[0];
                sourceInteractor.SetSelectionEffect(Interactor.SelectionEffect.Flip);
                sourceInteractor.Unselect();
                //sourceInteractor.SetSelectionEffect(Interactor.SelectionEffect.FlipAndShake);
                sourceInteractor.transform.DOMove(interactorsPivots[0].position, moveTime * gameSpeed, true);
                // If there another card to move then set the next step
                // Wait
                yield return new WaitForSeconds(moveTime);
                // Next
                count++;
                if (nextInteractor)
                {
                    Destroy(sourceInteractor.gameObject, moveTime * gameSpeed);
                    sourceInteractor = nextInteractor;
                    nextInteractor = null;
                }
                else
                {
                    sourceInteractor.SetSelectionEffect(Interactor.SelectionEffect.FlipAndPulse);
                    sourceInteractor = null;
                }

                
            }
            Interactor discardInteractor = Instantiate(interactorPrefab, interactorsPivots[1], false).GetComponent<Interactor>();
            interactors[1] = discardInteractor;
            interactable = true;
        }
        #endregion

        #region public methods
        public int GetIndex(Interactor interactor)
        {
            return interactors.FindIndex(i => i == interactor);
        }

        public void Interact(Interactor target)
        {
            Debug.LogFormat("[GameUI ClickingOn:{0}", target);
            if (!interactable)
                return;

            // You can not move from the angles ( aces spots )
            if(selected == null)
            {
                switch (GetIndex(target))
                {
                    case 3:
                    case 5:
                    case 7:
                    case 9:
                        return;
                }
            }

            // You can not select an empty card pile as first selection
            if (selected == null && Ruler.Instance.GetCardPileAt(GetIndex(target)).IsEmpty())
                return;

            // If the interactor is already selected and it's not the main pile you can unselect it
            if (selected == target)
            {
                // You can not unselect the main pile
                if(GetIndex(target) != 0) 
                {
                    selected.Unselect();
                    selected = null;
                }
                return;
            }

            Debug.LogFormat("[GameUI ClickingOn - BBBBBBBBBBBBBBBB:{0}", target);
            // If there is another interactor selected you must check if you can move the selected card
            // ( or cards ) on the new pile
            if (selected != null && selected != target)
            {
                
                Interactor sourceInteractor = selected;
                Interactor targetInteractor = target;

                if(TryMove(sourceInteractor, targetInteractor))
                {
                    selected.Unselect();
                    selected = null;
                }
                
               // Ruler.Instance.DebugAll();
                return;
            }

            selected = target;
            target.Select();
        }


        #endregion



    }

}
