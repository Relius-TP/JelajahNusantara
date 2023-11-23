using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionHandler : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown micDropdownMenu;
    private List<string> microphoneList;

    private void Start()
    {
        microphoneList = new List<string>();
        RefreshMicrophoneList();
    }

    private void RefreshMicrophoneList()
    {
        micDropdownMenu.options.Clear();
        microphoneList.Clear();

        foreach (var mic in Microphone.devices)
        {
            microphoneList.Add(mic);
        }

        micDropdownMenu.AddOptions(microphoneList);
    }
}
