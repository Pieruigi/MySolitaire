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
        #endregion

        private void Awake()
        {
            Ruler.Instance.DebugAll();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public int GetIndex(Interactor interactor)
        {
            return interactors.FindIndex(i => i == interactor);
        }
    }

}
