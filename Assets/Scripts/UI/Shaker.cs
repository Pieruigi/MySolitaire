using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.UI
{
    public class Shaker : MonoBehaviour
    {
        [SerializeField]
        float angle = 20f;
        
        [SerializeField]
        float time = 0.25f;

        bool playing = false;
        bool stopping = false;
        float dir = 1;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void Shake()
        {
            if (stopping)
            {
                stopping = false;
                playing = false;
                return;
            }

           
            dir *= -1;
            transform.DOLocalRotate(new Vector3(0, 0, dir*angle), time).SetLoops(2, LoopType.Yoyo).OnComplete(Shake);
        }

        // Effects
        public void Play()
        {
            if (playing)
                return;

            playing = true;
            dir = 1;

            
            Shake();

        }

        public void Stop()
        {
            stopping = true;
        }

        public bool IsPlaying()
        {
            return playing;
        }
    }

}
