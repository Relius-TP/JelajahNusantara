using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionHandler : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TMP_Dropdown micDropdownMenu;
    private List<string> microphoneList;

    private PlayerSettingsData playerSettingsData;

    private void Start()
    {
        microphoneList = new List<string>();
        playerSettingsData = new PlayerSettingsData();
        RefreshMicrophoneList();
    }

    public void RefreshMicrophoneList()
    {
        micDropdownMenu.options.Clear();
        microphoneList.Clear();

        foreach (var mic in Microphone.devices)
        {
            microphoneList.Add(mic);
        }

        micDropdownMenu.AddOptions(microphoneList);
    }

    public void UpdateSettings()
    {
        //playerSettingsData.bgmValue = bgmSlider.value;
        //playerSettingsData.sfxValue = sfxSlider.value;
        playerSettingsData.deviceName = micDropdownMenu.options[micDropdownMenu.value].text;
        FileSytemManager.instance.SavePlayerSettings(playerSettingsData);
    }
}
