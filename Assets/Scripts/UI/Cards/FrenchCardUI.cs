using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zoca.Logic;

namespace Zoca.UI
{
    public class FrenchCardUI : CardUI
    {

        /// <summary>
        /// Returns the back color of the deck.
        /// Format is: backName_[0|1]
        /// </summary>
        /// <param name="cards"></param>
        protected override Sprite GetBackSprite()
        {
            Sprite[] sprites = GameResourcesManager.Instance.GetSetOfCardsBackSprites();
            string str = string.Format("_{0}", FrenchCardUtility.GetDeckColor(Card));
            return new List<Sprite>(sprites).Find(c => c.name.EndsWith(str));
        }
    }


    
}
