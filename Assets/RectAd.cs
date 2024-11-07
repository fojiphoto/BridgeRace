using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using IdyllicGames;

public class RectAd : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        // AdManager_IdyllicGames.ShowMediumBanner();

        if(CASAds.instance)
        {
            CASAds.instance.ShowMrecBanner(CAS.AdPosition.BottomLeft);
        }
    }

    // Update is called once per frame
    void OnDisable()
    {
        //  AdManager_IdyllicGames.HideMediumBanner();

        if (CASAds.instance)
        {
            CASAds.instance.ShowMrecBanner(CAS.AdPosition.BottomLeft);
        }
    }

    private void OnDestroy()
    {
        if (CASAds.instance)
        {
            CASAds.instance.ShowMrecBanner(CAS.AdPosition.BottomLeft);
        }
    }
}
