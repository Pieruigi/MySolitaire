using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zoca.Logic
{
    /// <summary>
    /// Each card is represented by an internal array of string which is interpreted by some specific 
    /// utility classes ( ex. ItalianCardUtility ).
    /// </summary>
    public class Card
    {
        #region delegates
        public UnityAction OnSelected;
        public UnityAction OnUnselected;
        #endregion

        #region properties

        #endregion

        #region private fields
        // Internal representation of the card ( for ex. 1_0 might be used for the ace of heart )
        object[] dataArray = null;
        
        bool selected = false;
        #endregion

        #region public methods
        /// <summary>
        /// Create a new card given a specific internal representation
        /// </summary>
        /// <param name="data"></param>
        public Card(object[] dataArray)
        {
            // Add each value separated by the separator character
            this.dataArray = dataArray;
            
        }

        public int GetDataArrayLength()
        {
            return dataArray.Length;
        }

        /// <summary>
        /// Returns a specific value from the internal representation ( for example the value or the suit )
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object GetDataAt(int index)
        {
            return dataArray[index];
        }

        public void SetSelected(bool value)
        {
            selected = value;
            if (value)
                OnSelected?.Invoke();
            else
                OnUnselected?.Invoke();
        }
        


        public bool IsSelected()
        {
            return selected;
        }

        public override string ToString()
        {
            string s = null;
            foreach (object data in dataArray)
                s += string.IsNullOrEmpty(s) ? data.ToString() : string.Format("_{0}", data.ToString());
            return s;
        }
        #endregion
    }

}
