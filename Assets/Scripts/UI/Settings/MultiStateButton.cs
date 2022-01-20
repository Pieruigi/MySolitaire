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

        [SerializeField]
        List<string> texts;

        int state;
        int stateCount;
        Button button;
        Image image;
        Text text;
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
            text = GetComponentInChildren<Text>();

            // The number of the states is equal to the number of the sprites
            stateCount = sprites.Count;

            // Get the current state from the child
            state = GetCurrentState();

            // Set the corresponding sprite
            image.sprite = sprites[state];
            if (texts.Count > 0)
                text.text = texts[state];

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
            if (texts.Count > 0)
                text.text = texts[state];

            // Call update on child 
            OnStateChanged(state);
        }
        #endregion

    }

}
