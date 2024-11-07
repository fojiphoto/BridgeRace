using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onEnableOnDisable : MonoBehaviour
{
    private void OnEnable()
    {
        //CASAds.instance?.ShowMrecBanner(CAS.AdPosition.TopLeft);
        //AdsManager.instance?.ShowMRec();
    }
    private void OnDisable()
    {
        // AdsManager.instance?.HideMRec();
        //CASAds.instance?.HideMrecBanner();
    }
}
