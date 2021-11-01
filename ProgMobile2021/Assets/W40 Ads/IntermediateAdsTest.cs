using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateAdsTest : MonoBehaviour
{
    AdsManager m_AdsManager;
    private void Start()
    {
        m_AdsManager = FindObjectOfType<AdsManager>();
        InvokeRepeating("CallIntermediateAd", 5f, 30f);
    }
    private void CallIntermediateAd()
    {
        m_AdsManager.ShowAd(AdsManager.addTypes.interstitial);
    }
}
