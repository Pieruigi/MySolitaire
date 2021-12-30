using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Management
{
    /// <summary>
    /// Resource manager.
    /// </summary>
    public class ResourceManager : MonoBehaviour 
    {
        #region static constants fields
        public static readonly string DecksResourcesBaseFolder = "Decks";
        public static readonly string DeckResourcesFrontFolder = "Front";
        public static readonly string DeckResourcesBackFolder = "Back";
        static readonly string TablesResourcesBaseFolder = "Tables";

        public static ResourceManager Instance { get; private set; }
        #endregion

        #region private fields
        // Resources folder
        string deckResourceFolder = "Napoletane";
        //string tableResourceFolder = "Tables";

        // Save data
        string playerPrefsDeckName = "Deck";
        string playerPrefsTableName = "Table";
        #endregion

        #region private methods
        private void Awake()
        {
            if(!Instance)
            {
                Instance = this;

                // Try to get data from player prefs
                
                //if (PlayerPrefs.HasKey(playerPrefsTableName))
                //{
                //    tableResourceFolder = PlayerPrefs.GetString(playerPrefsTableName);
                //}
                //if (PlayerPrefs.HasKey(playerPrefsDeckName))
                //{
                //    deckResourceFolder = PlayerPrefs.GetString(playerPrefsDeckName);
                //}

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
        #endregion

        #region public methods
        /// <summary>
        /// Returns all the front sprites of the current set of cards.
        /// </summary>
        /// <returns></returns>
        public Sprite[] GetSetOfCardsFrontSprites()
        {
            string deckFolder = System.IO.Path.Combine(DecksResourcesBaseFolder, deckResourceFolder);
            deckFolder = System.IO.Path.Combine(deckFolder, DeckResourcesFrontFolder);
            return Resources.LoadAll<Sprite>(deckFolder);
        }

        /// <summary>
        /// Returns the backs of the current set of cards.
        /// They might be two sprites, back_0 and back_1, or one single sprite, back_0.
        /// Underscore is reserved for the color, so don't use it in the name:
        ///     back_great_0 -> wrong
        ///     backGreat_0 -> right.
        /// </summary>
        /// <returns></returns>
        public Sprite[] GetSetOfCardsBackSprites()
        {
            string deckFolder = System.IO.Path.Combine(DecksResourcesBaseFolder, deckResourceFolder);
            //deckFolder = System.IO.Path.Combine(deckFolder, DeckResourcesBackFolder);
            return Resources.LoadAll<Sprite>(deckFolder);
        }

        public Sprite[] GetTablesSprites()
        {
            string folder = TablesResourcesBaseFolder;
            return Resources.LoadAll<Sprite>(folder);
        }
        #endregion

    }

}
