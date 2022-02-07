using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Management;

namespace Zoca.UI
{
    public class StatsUI : MonoBehaviour
    {
        [SerializeField]
        Text valueText;

      
        // Start is called before the first frame update
        void Start()
        {
            valueText.text = StatsManager.Instance.NumOfVictories.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
