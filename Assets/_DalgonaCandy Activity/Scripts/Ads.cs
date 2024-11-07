using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ads : MonoBehaviour
{
    public bool isRevive = false;
    public Image Revive_Image;
    Sequence ImageSq;

    private void OnEnable()
    {
        if (isRevive)
        {
            ImageSq = DOTween.Sequence();
            ImageSq.Append( Revive_Image.DOFillAmount(0,5).SetEase(Ease.Linear));
            Invoke(nameof(CompleteTween),5) ;
        }
       
        //AdsManager.instance?.ShowInterstitialWithoutConditions();
        //AdsManager.instance?.ShowMRec();
        //CASAds.instance?.ShowMrecBanner(CAS.AdPosition.TopLeft);
    }

    private void OnDisable()
    {
        //AdsManager.instance?.HideMRec();
        //CASAds.instance?.HideMrecBanner();
    }

    public void StopTween() 
    {
    ImageSq.Kill();
    }

    public void CompleteTween()
    {
        //if (UiManager.instance.isRevived == false)
        //    UiManager.instance.ReviveNo();
    }


}
