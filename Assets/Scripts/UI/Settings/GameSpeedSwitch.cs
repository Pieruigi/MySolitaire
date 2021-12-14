using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zoca.Management;

namespace Zoca.UI
{
    public class GameSpeedSwitch : Switch
    {
        protected override void HandleOnValueChanged(bool value)
        {
            Debug.Log("MasterVolumeSetting - HandleOnValueChanged:" + value);
            if (!value)
                SettingsManager.Instance.SetGameSpeedNormal();
            else
                SettingsManager.Instance.SetGameSpeedFast();

        }

        protected override void InitToggle()
        {
            // Get setting value
            Toggle.isOn = SettingsManager.Instance.IsGameSpeedFast();
            
        }
    }

}
