using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Zoca.Management
{
    public class StatsManager : MonoBehaviour
    {
        public const string StatsParamName = "stats";

        public static StatsManager Instance { get; private set; }

        public int NumOfVictories
        {
            get { return numOfVictories; }
        }
        int numOfVictories;

        string file;

        // Start is called before the first frame update
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;

                Instance.file = Application.persistentDataPath + "/stats";

                //if (!File.Exists(file))
                //    File.Create(file);

                //string stats = PlayerPrefs.GetString(StatsParamName, "0");
                string stats = ReadFile(file);

                if (string.IsNullOrEmpty(stats))
                    stats = "0";

                //WriteFile(file, stats);

                numOfVictories = int.Parse(stats);

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        

        public void UpdateStats()
        {
            numOfVictories++;
            WriteFile(file, numOfVictories.ToString());
            //PlayerPrefs.SetString(StatsParamName, numOfVictories.ToString());
            //PlayerPrefs.Save();
        }

        void WriteFile(string file, string data)
        {
            File.WriteAllText(file, data);
        }

        string ReadFile(string file)
        {
            if (!File.Exists(file))
                return "";

            return File.ReadAllText(file);
        }
    }

}
