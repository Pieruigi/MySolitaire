using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Management
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
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
