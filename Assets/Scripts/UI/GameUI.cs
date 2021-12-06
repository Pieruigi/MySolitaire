using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        #endregion

        private void Awake()
        {
            Ruler.Instance.DebugAll();
        }

        // Start is called before the first frame update
        void Start()
        {
            interactable = true;
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
            // If the interactor is already selected you unselect it, unless it is the main pile interactor
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

            // If there is another interactor selected you must first unselect it
            if(selected != null && selected != interactor)
            {
                return;
            }

            selected = interactor;
            interactor.Select();
        }
    }

}
