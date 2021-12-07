using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Logic;

namespace Zoca.UI
{
    public class CardUI : MonoBehaviour
    {
        #region properties
        [Header("Effects")]
        [SerializeField]
        float flipTime = 1f;

        [SerializeField]
        float shakeAngle = 20f;

        [SerializeField]
        float shakeTime = 0.25f;
        #endregion

        #region private fields
        Card card;

        // Graphics fields
        Image image;
        Sprite frontSprite;
        Sprite backSprite;


        // Shaking fields
        bool shaking = false;
        bool stopShaking = false;
        float shakeDir = 1;
        #endregion

        #region private methods
        private void Awake()
        {
            image = GetComponent<Image>();
          
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        Sprite GetFrontSprite()
        {
            // Set image
            Sprite[] sprites = ResourceManager.Instance.GetSetOfCardsFrontSprites();
            Debug.LogFormat("ItalianCardUI.GetFrontSprite() - sprites.Length:{0}", sprites.Length);
            string valueStr = string.Format("{0:000}", card.Value);
            string suitStr = string.Format("{0:000}", card.Suit);
            string str = string.Format("{0}_{1}", valueStr, suitStr);
            Debug.LogFormat("ItalianCardUI - Looking for sprite [name:{0}]", str);
            // Get the first sprite starting with the valueStr.
            // If the deck has multiple suits then the sprite with suit 000 is taken.
            return new List<Sprite>(sprites).Find(c => c.name.StartsWith(str));
        }

        Sprite GetBackSprite()
        {
            Sprite[] sprites = ResourceManager.Instance.GetSetOfCardsBackSprites();
            return sprites[0];
        }

        

        

        void Shake()
        {
            if (stopShaking)
            {
                stopShaking = false;
                shaking = false;
                return;
            }

            shakeDir *= -1;
            transform.DOLocalRotate(new Vector3(0, 0, shakeDir * shakeAngle), shakeTime).SetLoops(2, LoopType.Yoyo).OnComplete(Shake);
        }
        #endregion

        #region public methods
        public void StartShaking()
        {
            if (shaking)
                return;

            shaking = true;
            shakeDir = 1;

            Shake();

        }

        public void StopShaking()
        {
            stopShaking = true;
        }

        public bool IsShaking()
        {
            return shaking;
        }

        public void SetCard(Card card)
        {
            this.card = card;
            // Get back and front sprites
            frontSprite = GetFrontSprite();
            backSprite = GetBackSprite();
        }

        public void ShowFront()
        {
            image.sprite = frontSprite;
            image.enabled = true;
        }

        public void ShowBack()
        {
            image.sprite = backSprite;
            image.enabled = true;
        }

        public bool IsFront()
        {
            return image.sprite == frontSprite;
        }


        public IEnumerator Flip()
        {
            float oldX = transform.localScale.x;
            // Flip out
            yield return transform.DOScaleX(0, flipTime).WaitForCompletion();

            // Set sprite
            if (IsFront())
                ShowBack();
            else
                ShowFront();

            // Flip in
            yield return transform.DOScaleX(oldX, flipTime).WaitForCompletion();
        }

        public IEnumerator FlipAndStartShaking()
        {
            yield return Flip();

            StartShaking();
        }

        public IEnumerator StopShakingAndFlip()
        {
            StopShaking();
            while (IsShaking())
            {
                yield return null;
            }
            yield return Flip();
        }
        #endregion


    }

}
