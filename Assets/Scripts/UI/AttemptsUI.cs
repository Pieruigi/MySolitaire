using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Logic;

namespace Zoca.UI
{
    public class AttemptsUI : MonoBehaviour
    {
        [SerializeField]
        Text textField;

        

        string singularTextFormat = "{0} mossa rimasta";
        string pluralTextFormat = "{0} mosse rimaste";

        DateTime lastTime;
       
        private void Awake()
        {
            textField.enabled = false;
            textField.transform.parent.GetComponent<Image>().enabled = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            Ruler.Instance.OnAttemptsLeftChanged += Show;


        }

        // Update is called once per frame
        void Update()
        {
            if (textField.enabled)
            {
                if ((DateTime.UtcNow - lastTime).TotalSeconds > 2)
                {
                    textField.enabled = false;
                    textField.transform.parent.GetComponent<Image>().enabled = false;
                }
                    
            }
            
        }

        void Show(int attemptsLeft)
        {
            
            textField.enabled = true;
            textField.transform.parent.GetComponent<Image>().enabled = true;

            if (attemptsLeft == 1)
                textField.text = string.Format(singularTextFormat, attemptsLeft);
            else
                textField.text = string.Format(pluralTextFormat, attemptsLeft);

            lastTime = DateTime.UtcNow;
            
        }

        
    }

}
