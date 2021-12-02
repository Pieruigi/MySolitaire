using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zoca.Logic;

namespace Zoca.UI
{
    public class ItalianCardUI : CardUI
    {

        /// <summary>
        /// Italian card front sprite has to take into account both value and suit ( while the simple card only
        /// takes into account the value ).
        /// </summary>
        /// <param name="cards"></param>
        protected override Sprite GetFrontSprite()
        {
            Sprite[] sprites = GameResourcesManager.Instance.GetSetOfCardsFrontSprites();
            Debug.LogFormat("ItalianCardUI.GetFrontSprite() - sprites.Length:{0}", sprites.Length);
            string valueStr = string.Format("{0:000}", ItalianCardUtility.GetValue(Card));
            string suitStr = string.Format("{0:000}", ItalianCardUtility.GetSuit(Card));
            string str = string.Format("{0}_{1}", valueStr, suitStr);
            Debug.LogFormat("ItalianCardUI - Looking for sprite [name:{0}]", str);
            // Get the first sprite starting with the valueStr.
            // If the deck has multiple suits then the sprite with suit 000 is taken.
            return new List<Sprite>(sprites).Find(c => c.name.StartsWith(str));
        }

      
    }


    
}
