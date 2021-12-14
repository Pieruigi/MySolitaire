using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zoca.UI
{
    public abstract class Switch : MonoBehaviour
    {
        public Toggle Toggle
        {
            get { return toggle; }
        }

        Toggle toggle;

        protected abstract void InitToggle();
        protected abstract void HandleOnValueChanged(bool value);

        protected virtual void Awake()
        {
            toggle = GetComponent<Toggle>();
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            InitToggle();

            toggle.onValueChanged.AddListener(HandleOnValueChanged);
        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }
    }

}
