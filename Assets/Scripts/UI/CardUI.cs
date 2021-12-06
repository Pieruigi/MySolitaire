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
        [SerializeField]
        float flipTime = 1f;

        Card card;
        Image image;
      
        Sprite frontSprite;
        Sprite backSprite;

 

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

        IEnumerator Flip()
        {
            float oldX = transform.localScale.x;
            // Flip out
            yield return transform.DOScaleX(0, flipTime);

            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

            // Set sprite
            if (IsFront())
                ShowBack();
            else
                ShowFront();

            // Flip in
            yield return transform.DOScaleX(oldX, flipTime);

            GetComponent<Shaker>().Play();
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
            return image == frontSprite;
        }

        public void Select()
        {
            if (IsFront())
            {
                GetComponent<Shaker>().Play();
            }
            else
            {
                // Flip card
                StartCoroutine(Flip());
            }
        }

        public void Unselect()
        {
            GetComponent<Shaker>().Stop();
        }


    }

}
