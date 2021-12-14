using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zoca.UI
{
    public abstract class MultiStateButton : MonoBehaviour
    {
        #region properties
        public Button Button
        {
            get { return button; }
        }
        #endregion

        #region private fields
        [SerializeField]
        List<Sprite> sprites;

        int state;
        int stateCount;
        Button button;
        Image image;
        #endregion

        #region protected methods
        protected abstract int GetCurrentState();

        protected abstract void OnStateChanged(int newState);

        
        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            image = GetComponent<Image>();

            
        }

        protected virtual void Start()
        {
            // The number of the states is equal to the number of the sprites
            stateCount = sprites.Count;

            // Get the current state from the child
            state = GetCurrentState();

            // Set the corresponding sprite
            image.sprite = sprites[state];

            // Add the on click listener
            button.onClick.AddListener(HandleOnClick);
        }
        #endregion

        #region private methods
        void HandleOnClick()
        {
            // Update the state
            state++;
            if (state >= stateCount)
                state = 0;

            // Change the sprite
            image.sprite = sprites[state];

            // Call update on child 
            OnStateChanged(state);
        }
        #endregion

    }

}
