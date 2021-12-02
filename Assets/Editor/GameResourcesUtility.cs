using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Zoca.Editor
{
    /// <summary>
    /// This is a helper class for cards resources
    /// </summary>
    public class GameResourcesUtility
    {
        

        /// <summary>
        /// Renames all the sprites in a specific resource folder.
        /// Name format is like 02_04, where 02 is the value and 04 the suit.
        /// We need the sprites to be in order while renaming its.
        /// </summary>
        /// <param name="suitCount"></param>
        [MenuItem("Tools/Game/Utility/RenameFrontDeckSprites")]
        public static void RenameFrontDeckSprites()
        {
            Debug.Log(string.Format("{0:000}", 1));

            string folder = "E:/backup/VG Projects/Assets/Convert/";
            int suitCount = 4;

            if (!System.IO.Directory.Exists(folder))
            {
                Debug.LogError("Directory not found: " + folder);
                return;
            }
                

            // Get all files
            string[] files = System.IO.Directory.GetFiles(folder);
            Debug.LogFormat("files.Length:" + files.Length);
            Debug.LogFormat("file[0]:" + files[0]);

            int maxValue = files.Length / suitCount;

            // Move all files
            int count = 0;
            for(int suit=0; suit<suitCount; suit++)
            {
                for(int value=1; value<maxValue+1; value++)
                {
                    // Get the base path
                    string path = files[count].Substring(0, files[count].LastIndexOf("/"));
                    // Get the file extension
                    string fileExt = files[count].Substring(files[count].LastIndexOf("."));
                    // Build the new file name
                    string fileName = string.Format("{0:000}_{1:000}", value, suit);
                    // Create the new file path
                    string newFile = System.IO.Path.Combine(path, fileName) + fileExt;
                    Debug.LogFormat("NewFile:{0}", newFile);
                    // Rename the file
                    System.IO.File.Move(files[count], newFile);

                    count++;
                }
            }

           
            

        }

        
       
    }

}
