using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zoca.Management;

namespace Zoca.UI
{
    public class MasterVolumeSwitch : Switch
    {
        protected override void HandleOnValueChanged(bool value)
        {
            Debug.Log("MasterVolumeSetting - HandleOnValueChanged:" + value);
            if (value)
                SettingsManager.Instance.SetMasterVolumeOn();
            else
                SettingsManager.Instance.SetMasterVolumeOff();

        }

        protected override void InitToggle()
        {
            // Get setting value
            Toggle.isOn = SettingsManager.Instance.IsMasterVolumeOn();
            
        }
    }

}
