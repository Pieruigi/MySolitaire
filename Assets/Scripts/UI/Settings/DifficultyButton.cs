using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zoca.Management;

namespace Zoca.UI
{
    public class DifficultyButton : MultiStateButton
    {
        
        protected override int GetCurrentState()
        {
            // Get settings
            return SettingsManager.Instance.Difficulty;
        }

        protected override void OnStateChanged(int newState)
        {
            SettingsManager.Instance.SetDifficulty(newState);
        }
    }

}
