using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca.Test
{
    /// <summary>
    /// Input example: 
    ///         2d,3b,8c,6s|3d|||7c,6d|...  ( piles )
    ///         sel:8c        ( selected card )
    /// 
    /// Output exampre: 0,1,0,0,1,... (pile 1 not interactable, pile 2 interactable, ecc. )
    /// Card pile ids:
    ///     0: main 
    ///     1: discard 
    ///     2: north 
    ///     3: north east
    ///     4: east 
    ///     5: south east 
    ///     6: south
    ///     7: south west
    ///     8: west
    ///     9: north west
    ///     10: north
    /// </summary>
    public class TestMySolitaireLogic : MonoBehaviour
    {
        string testString = null;


        private void Awake()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
