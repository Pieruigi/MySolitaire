using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Management;

namespace Zoca.UI
{
    public class ButtonReview : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(OpenReviewUrl);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OpenReviewUrl()
        {
            ReviewManager.Instance.OpenReviewUrl();
        }
    }

}
