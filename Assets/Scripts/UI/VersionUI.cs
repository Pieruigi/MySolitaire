using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zoca.UI
{
    public class VersionUI : MonoBehaviour
    {
        [SerializeField]
        Text versionText;
        private void Awake()
        {
            versionText.text = Application.version;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
