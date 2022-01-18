using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zoca.Management;

namespace Zoca.UI
{
    public class ReviewUI : MonoBehaviour
    {
        [SerializeField]
        GameObject panel;

        private void Awake()
        {
            ReviewManager.Instance.OnAskForReview += OpenReviewPanel;

        }

        // Start is called before the first frame update
        void Start()
        {
            panel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            ReviewManager.Instance.OnAskForReview -= OpenReviewPanel;
        }

        void OpenReviewPanel()
        {
            panel.SetActive(true);
        }

        public void Review()
        {
            ReviewManager.Instance.OpenReviewUrl();
        }
    }

}
