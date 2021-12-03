using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca
{
    public class GameFactory : MonoBehaviour
    {
        static readonly string GameLogicResourcesFolder = "Factory/Logic/Games";
        static readonly string GameUIResourcesFolder = "Factory/UI/Games";

        private void Awake()
        {

#if MY_SOLITAIRE
            string name = "MySolitaire";
#endif

            
            // Load ui
            GameObject g = Resources.Load<GameObject>(System.IO.Path.Combine(GameUIResourcesFolder, string.Format("{0}GameUI", name)));
            // Create ui
            Instantiate(g, Vector3.zero, Quaternion.identity);

            // Load game logic
            g = Resources.Load<GameObject>(System.IO.Path.Combine(GameLogicResourcesFolder, string.Format("{0}GameLogic", name)));
            // Create the game logic
            Instantiate(g, Vector3.zero, Quaternion.identity);
        }
    }

}
