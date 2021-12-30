using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Management;

namespace Zoca.UI
{
    public class Table : MonoBehaviour
    {
        private void Awake()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
            Sprite[] sprites = ResourceManager.Instance.GetTablesSprites();
            GetComponent<Image>().sprite = sprites[Random.Range(0, sprites.Length)];
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
