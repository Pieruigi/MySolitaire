using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zoca.Logic;

namespace Zoca.UI
{
    public abstract class GameUI : MonoBehaviour
    {

        #region properties
        public static GameUI Instance { get; private set; }

        public CardUI CardPrefab
        {
            get { return cardPrefab; }
        }
        #endregion

        #region private fields
        [SerializeField]
        CardUI cardPrefab; // The card used in the game


        #endregion

        #region protected methods
        public abstract void OnPointerDown(IPointerDownHandler handler, PointerEventData eventData);
        public abstract void OnPointerUp(IPointerUpHandler handler, PointerEventData eventData);

        protected virtual void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
           
        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        protected virtual void LateUpdate()
        {

        }
        #endregion
    }

}
