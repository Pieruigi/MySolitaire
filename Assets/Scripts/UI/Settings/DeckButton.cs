using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zoca.Management;

namespace Zoca.UI
{
    public class DeckButton : MultiStateButton
    {
        
        protected override int GetCurrentState()
        {
            // Get settings
            return SettingsManager.Instance.DeckId;
        }

        protected override void OnStateChanged(int newState)
        {
            SettingsManager.Instance.SetDeckId(newState);
        }
    }

}
