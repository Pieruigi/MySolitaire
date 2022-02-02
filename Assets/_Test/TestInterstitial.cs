using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoca.Management;

public class TestInterstitial : MonoBehaviour
{
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { AdsManager.Instance.TryShowInterstitial(HandleOnInterstitialCloded); button.interactable = false; });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleOnInterstitialCloded()
    {
        GetComponent<Button>().interactable = true;
    }
}
